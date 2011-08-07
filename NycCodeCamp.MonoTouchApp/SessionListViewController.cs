using System;
using System.Linq;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;
using System.Collections.Generic;

namespace NycCodeCamp.MonoTouchApp
{
	public class SessionListViewController : UITableViewController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			Title = "Sessions";
			
			var allSessions = AppDelegate.CodeCampRepository.GetSessions();
			
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
				
				cell.TextLabel.Text = session.Title;
				cell.DetailTextLabel.Text = session.Speaker.Name;
				
				return cell;
			}
			
			public override string TitleForHeader(UITableView tableView, int section)
			{
				return _sessions.Keys.ElementAt(section);
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

