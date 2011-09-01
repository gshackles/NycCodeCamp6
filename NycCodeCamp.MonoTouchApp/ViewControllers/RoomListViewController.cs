using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using CodeCamp.Core.Entities;

namespace NycCodeCamp.MonoTouchApp
{
	public class RoomListViewController : ListControllerBase
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			Title = "Rooms";
			
			var rooms = AppDelegate.CodeCampService.Repository.GetRooms();
			TableView.Source = new RoomTableViewSource(this, rooms);
		}
		
		private class RoomTableViewSource : UITableViewSource
		{
			private readonly RoomListViewController _hostController;
			private readonly IList<Room> _rooms;
			private const string ROOM_CELL = "roomCell";
			
			public RoomTableViewSource (RoomListViewController hostController, IList<Room> rooms)
			{
				_hostController = hostController;
				_rooms = rooms;
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return _rooms.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(ROOM_CELL)
							?? new UITableViewCell(UITableViewCellStyle.Subtitle, ROOM_CELL);
				
				var room = _rooms[indexPath.Row];
				
				cell.TextLabel.Text = room.Name;
				cell.DetailTextLabel.Text = room.Description;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				cell.BackgroundView = new UIView(cell.Frame) { BackgroundColor = UIColor.White };
				
				return cell;
			}
			
			public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				_hostController.NavigationController.PushViewController(
					new RoomViewController(_rooms[indexPath.Row]), true);
			}
			
			public override float GetHeightForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				return 65;
			}
		}
	}
}

