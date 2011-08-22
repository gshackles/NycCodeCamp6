using System;
using System.Collections.Generic;
using System.Linq;
using CodeCamp.Core;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;
using CodeCamp.Core.ViewModels;
using System.Drawing;

namespace NycCodeCamp.MonoTouchApp
{
	public class SponsorListViewController : ListControllerBase
	{
		private IList<Sponsor> _sponsors;
		private IList<string> _tiers;
		
		public SponsorListViewController (IList<Sponsor> sponsors, IList<string> tiers)
			: base(UITableViewStyle.Grouped)
		{
			_sponsors = sponsors;
			_tiers = tiers;
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			Title = "Sponsors";
			TableView.Source = new SponsorTableViewSource(this, _sponsors, _tiers);
		}
		
		private class SponsorTableViewSource : UITableViewSource
		{
			private readonly SponsorListViewModel _viewModel; 
			private const string SPONSOR_CELL = "sponsorCell";
			private SponsorListViewController _hostController;
			
			public SponsorTableViewSource(SponsorListViewController hostController, IList<Sponsor> sponsors, IList<string> tiers)
			{
				_viewModel = new SponsorListViewModel(tiers, sponsors);
				_hostController = hostController;
			}
			
			public override int NumberOfSections(UITableView tableView)
			{
				return _viewModel.Tiers.Count;
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return _viewModel.Tiers[section].Sponsors.Count;
			}
			
			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(SPONSOR_CELL)
							?? new UITableViewCell(UITableViewCellStyle.Default, SPONSOR_CELL);
				
				cell.TextLabel.Text = _viewModel.Tiers[indexPath.Section].Sponsors[indexPath.Row].Name;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				
				return cell;
			}
			
			public override UIView GetViewForHeader(UITableView tableView, int section)
			{
				var headerView = new UIView(new RectangleF(0, 0, tableView.Bounds.Size.Width, 50));
				var headerLabel = new UILabel(new RectangleF(15, 0, headerView.Frame.Width - 15, headerView.Frame.Height));
				
				headerLabel.Text = _viewModel.Tiers[section].Name;
				headerLabel.BackgroundColor = UIColor.Clear;
				headerLabel.TextColor = UIColor.White;
				headerLabel.ShadowColor = UIColor.LightGray;
				
				headerView.AddSubview(headerLabel);
				
				return headerView;
			}
			
			public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var selectedSponsor = _viewModel.Tiers[indexPath.Section].Sponsors[indexPath.Row];
				var sponsorController = new SponsorViewController(selectedSponsor);
				
				_hostController.NavigationController.PushViewController(
					sponsorController, true);
			}
			
			public override float GetHeightForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				return 55;
			}
			
			public override float GetHeightForHeader(UITableView tableView, int section)
			{
				return 50;
			}
		}
	}
}

