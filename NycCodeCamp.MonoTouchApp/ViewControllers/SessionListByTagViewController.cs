using System.Collections.Generic;
using CodeCamp.Core.Entities;
using MonoTouch.UIKit;

namespace NycCodeCamp.MonoTouchApp
{
	public class SessionListByTagViewController : ListControllerBase
	{
		private string _tagName;
		
		public SessionListByTagViewController(string tagName)
		{
			_tagName = tagName;
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			Title = _tagName;
			
			var sessionsInTag = AppDelegate.CodeCampRepository.GetSessionsByTag(_tagName);
			
			TableView.Source = new SessionsByTagTableViewSource(this, sessionsInTag);
		}
		
		private class SessionsByTagTableViewSource : UITableViewSource
		{
			private IList<Session> _sessions;
			private SessionListByTagViewController _hostController;
			private const string SESSION_CELL = "sessionCell";
			
			public SessionsByTagTableViewSource (SessionListByTagViewController hostController, IList<Session> sessions)
			{
				_sessions = sessions;
				_hostController = hostController;
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return _sessions.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(SESSION_CELL)
							?? new UITableViewCell(UITableViewCellStyle.Subtitle, SESSION_CELL);
				var session = _sessions[indexPath.Row];
				
				cell.TextLabel.Text = session.Title;
				cell.DetailTextLabel.Text = string.Format("{0} - {1}",
														  session.Starts.ToLocalTime().ToShortTimeString(),
														  session.Ends.ToLocalTime().ToShortTimeString());
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				cell.BackgroundView = new UIView(cell.Frame) { BackgroundColor = UIColor.White };
				
				return cell;
			}
			
			public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var selectedSession = _sessions[indexPath.Row];
				
				_hostController.NavigationController.PushViewController(
					new SessionViewController(selectedSession), true);
			}
			
			public override float GetHeightForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				return 65;
			}
		}
	}
}

