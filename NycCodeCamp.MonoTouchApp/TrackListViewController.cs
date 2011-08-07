using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace NycCodeCamp.MonoTouchApp
{
	public class TrackListViewController : ListControllerBase
	{
		private IList<string> _tracks;
		
		public TrackListViewController(IList<string> tracks)
		{
			_tracks = tracks;
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			Title = "Tracks";
			TableView.Source = new TrackTableViewSource(this, _tracks);
		}
		
		private class TrackTableViewSource : UITableViewSource
		{
			private IList<string> _tracks;
			private TrackListViewController _hostController;
			private const string TRACK_CELL = "trackCell";
			
			public TrackTableViewSource (TrackListViewController hostController, IList<string> tracks)
			{
				_tracks = tracks;
				_hostController = hostController;
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return _tracks.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(TRACK_CELL)
							?? new UITableViewCell(UITableViewCellStyle.Default, TRACK_CELL);
				
				cell.TextLabel.Text = _tracks[indexPath.Row];
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				
				return cell;
			}
			
			public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				_hostController.NavigationController.PushViewController(
					new SessionListByTrackViewController(_tracks[indexPath.Row]), true);
			}
			
			public override float GetHeightForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				return 55;
			}
		}
	}
}

