using System.Collections.Generic;
using CodeCamp.Core.Entities;
using MonoTouch.UIKit;

namespace NycCodeCamp.MonoTouchApp
{
	public class SessionListByTrackViewController : ListControllerBase
	{
		private string _trackName;
		
		public SessionListByTrackViewController(string trackName)
		{
			_trackName = trackName;
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			Title = _trackName;
			
			var sessionsInTrack = AppDelegate.CodeCampRepository.GetSessionsByTrack(_trackName);
			
			TableView.Source = new SessionsByTrackTableViewSource(this, sessionsInTrack);
		}
		
		private class SessionsByTrackTableViewSource : UITableViewSource
		{
			private IList<Session> _sessions;
			private SessionListByTrackViewController _hostController;
			private const string SESSION_CELL = "sessionCell";
			
			public SessionsByTrackTableViewSource (SessionListByTrackViewController hostController, IList<Session> sessions)
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

