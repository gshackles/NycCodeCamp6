using System;
using System.Collections.Generic;
using System.Linq;
using CodeCamp.Core;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NycCodeCamp.MonoTouchApp
{
	public class SponsorListViewController : ListControllerBase
	{
		private IList<Sponsor> _sponsors;
		public SponsorListViewController (IList<Sponsor> sponsors)
		{
			_sponsors = sponsors;
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			Title = "Sponsors";
			TableView.Source = new SponsorTableViewSource(_sponsors);
		}
		
		private class SponsorTableViewSource : UITableViewSource
		{
			private IList<Sponsor> _sponsors;
			private const string SPONSOR_CELL = "sponsorCell";
			
			public SponsorTableViewSource(IList<Sponsor> sponsors)
			{
				_sponsors = sponsors.OrderBy(sponsor => sponsor.Name).ToList();
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return _sponsors.Count;
			}
			
			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(SPONSOR_CELL)
							?? new UITableViewCell(UITableViewCellStyle.Default, SPONSOR_CELL);
				
				cell.TextLabel.Text = _sponsors[indexPath.Row].Name;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				cell.BackgroundView = new UIView(cell.Frame) { BackgroundColor = UIColor.White };
				
				return cell;
			}
			
			public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var url = new NSUrl(_sponsors[indexPath.Row].Website);
				
				UIApplication.SharedApplication.OpenUrl(url);				
			}
			
			public override float GetHeightForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				return 55;
			}
		}
	}
}
