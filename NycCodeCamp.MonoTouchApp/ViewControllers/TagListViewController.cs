using System;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace NycCodeCamp.MonoTouchApp
{
	public class TagListViewController : ListControllerBase
	{
		private IList<string> _tags;
		
		public TagListViewController(IList<string> tags)
		{
			_tags = tags;
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			Title = "Tags";
			TableView.Source = new TagTableViewSource(this, _tags);
		}
		
		private class TagTableViewSource : UITableViewSource
		{
			private IList<string> _tags;
			private TagListViewController _hostController;
			private const string TAG_CELL = "tagCell";
			
			public TagTableViewSource (TagListViewController hostController, IList<string> tags)
			{
				_tags = tags;
				_hostController = hostController;
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return _tags.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(TAG_CELL)
							?? new UITableViewCell(UITableViewCellStyle.Default, TAG_CELL);
				
				cell.TextLabel.Text = _tags[indexPath.Row];
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				cell.BackgroundView = new UIView(cell.Frame) { BackgroundColor = UIColor.White };
				
				return cell;
			}
			
			public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				_hostController.NavigationController.PushViewController(
					new SessionListByTagViewController(_tags[indexPath.Row]), true);
			}
			
			public override float GetHeightForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				return 55;
			}
		}
	}
}

