using System;
using System.Linq;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;
using System.Collections.Generic;
using System.Drawing;

namespace NycCodeCamp.MonoTouchApp
{
	public class SessionListViewController : ListControllerBase
	{
		public SessionListViewController ()
			: base(UITableViewStyle.Grouped)
		{
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			Title = "Schedule";
			
			var allSessions = AppDelegate.CodeCampService.Repository.GetSessions();
			
			TableView.Source = new SessionsByTrackTableViewSource(this, allSessions);
		}
		
		private class SessionsByTrackTableViewSource : UITableViewSource
		{
			private Dictionary<string, List<Session>> _sessions;
			private SessionListViewController _hostController;
			private const string SESSION_CELL = "sessionCell";
			
			public SessionsByTrackTableViewSource (SessionListViewController hostController, IList<Session> sessions)
			{
				_hostController = hostController;
				
				_sessions =
					(
						from session in sessions
						group session by new { session.Starts, session.Ends } into timeSlot
						orderby timeSlot.Key.Starts, timeSlot.Key.Ends
						select new {
							TimeSlot = string.Format("{0} - {1}",
										  			 timeSlot.Key.Starts.ToLocalTime().ToShortTimeString(),
										  			 timeSlot.Key.Ends.ToLocalTime().ToShortTimeString()),
							Sessions = timeSlot.ToList()
						}
					).ToDictionary(slot => slot.TimeSlot, slot => slot.Sessions);
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return _sessions[_sessions.Keys.ElementAt(section)].Count;
			}
			
			public override int NumberOfSections(UITableView tableView)
			{
				return _sessions.Keys.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(SESSION_CELL)
							?? new UITableViewCell(UITableViewCellStyle.Subtitle, SESSION_CELL);
				var session = getSession(indexPath);
				
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				cell.TextLabel.Text = session.Title;
				cell.DetailTextLabel.Text = session.Speaker.Name;
				
				return cell;
			}
			
			public override float GetHeightForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				return 65;
			}
			
			public override float GetHeightForHeader(UITableView tableView, int section)
			{
				return 50;
			}
			
			public override UIView GetViewForHeader(UITableView tableView, int section)
			{
				var headerView = new UIView(new RectangleF(0, 0, tableView.Bounds.Size.Width, 50));
				var headerLabel = new UILabel(new RectangleF(15, 0, headerView.Frame.Width - 15, headerView.Frame.Height));
				
				headerLabel.Text = _sessions.Keys.ElementAt(section);
				headerLabel.BackgroundColor = UIColor.Clear;
				headerLabel.TextColor = UIColor.White;
				headerLabel.ShadowColor = UIColor.LightGray;
				
				headerView.AddSubview(headerLabel);
				
				return headerView;
			}
			
			public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var selectedSession = getSession(indexPath);
				
				_hostController.NavigationController.PushViewController(
					new SessionViewController(selectedSession), true);
			}
			
			private Session getSession(MonoTouch.Foundation.NSIndexPath indexPath) 
			{
				return _sessions[_sessions.Keys.ElementAt(indexPath.Section)].ElementAt(indexPath.Row);
			}
		}
	}
}

