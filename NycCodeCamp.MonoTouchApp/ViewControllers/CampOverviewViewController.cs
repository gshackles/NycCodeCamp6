using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeCamp.Core;
using CodeCamp.Core.Entities;
using MonoTouch.UIKit;
using CodeCamp.Core.ViewModels;

namespace NycCodeCamp.MonoTouchApp
{
	public class CampOverviewViewController : ListControllerBase
	{
		public CampOverviewViewController()
			: base(MonoTouch.UIKit.UITableViewStyle.Grouped)
		{
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			var refreshButton = new UIBarButtonItem(UIBarButtonSystemItem.Refresh, 
													(s, e) => AppDelegate.CodeCampService.CheckForUpdatedSchedule());
			NavigationItem.RightBarButtonItem = refreshButton;
		}
		
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear (animated);

			var allSessions = AppDelegate.CodeCampService.Repository.GetSessions();

			NavigationItem.Title = "NYC Code Camp";
			TableView.Source = new OverviewTableViewSource(this, allSessions);
			TableView.BackgroundColor = UIColor.Clear;
		}
		
		private class OverviewTableViewSource : UITableViewSource
		{
			private CampOverviewViewController _hostController;
			private const string OVERVIEW_SCHEDULE_CELL = "overviewScheduleCell";
			private const string OVERVIEW_LINK_CELL = "overviewLinkCell";
			private readonly CampOverviewViewModel _viewModel;
			
			public OverviewTableViewSource (CampOverviewViewController hostController, IList<Session> allSessions)
			{
				_hostController = hostController;
				_viewModel = new CampOverviewViewModel(allSessions);
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return section < _viewModel.UpcomingSlots.Count
						? _viewModel.UpcomingSlots[section].Sessions.Count
						: 1;
			}

			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				if (indexPath.Section < _viewModel.UpcomingSlots.Count)
				{
					var cell = tableView.DequeueReusableCell(OVERVIEW_SCHEDULE_CELL)
								?? new UITableViewCell(UITableViewCellStyle.Subtitle, OVERVIEW_SCHEDULE_CELL);
					var session = _viewModel.UpcomingSlots[indexPath.Section].Sessions[indexPath.Row];
					
					cell.TextLabel.Text = session.Title;
					cell.DetailTextLabel.Text = session.Speaker.Name;
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
					
					return cell;
				}
				else
				{
					var cell = tableView.DequeueReusableCell(OVERVIEW_LINK_CELL)
								?? new UITableViewCell(UITableViewCellStyle.Default, OVERVIEW_LINK_CELL);
					
					cell.TextLabel.Text = "Full Schedule";
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
					
					return cell;
				}
			}
			
			public override void RowSelected(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				if (indexPath.Section < _viewModel.UpcomingSlots.Count)
				{
					var selectedSession = _viewModel.UpcomingSlots[indexPath.Section].Sessions[indexPath.Row];
					
					_hostController.NavigationController.PushViewController(
						new SessionViewController(selectedSession), true);
				}
				else
				{
					_hostController.TabBarController.SelectedIndex = 1;
				}
			}
			
			public override float GetHeightForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				return indexPath.Section < _viewModel.UpcomingSlots.Count
						? 65
						: 55;
			}
			
			public override float GetHeightForHeader(UITableView tableView, int section)
			{
				return 50;
			}
			
			public override UIView GetViewForHeader(UITableView tableView, int section)
			{
				string headerText = getTitleForSectionHeader(section);
				
				if (string.IsNullOrEmpty(headerText))
					return null;
				
				var headerView = new UIView(new RectangleF(0, 0, tableView.Bounds.Size.Width, 50));
				var headerLabel = new UILabel(new RectangleF(15, 0, headerView.Frame.Width - 15, headerView.Frame.Height));
				
				headerLabel.Text = headerText;
				headerLabel.BackgroundColor = UIColor.Clear;
				headerLabel.TextColor = UIColor.White;
				headerLabel.ShadowColor = UIColor.LightGray;
				
				headerView.AddSubview(headerLabel);
				
				return headerView;
			}
			
			private string getTitleForSectionHeader(int section)
			{
				if (section < _viewModel.UpcomingSlots.Count)
				{
					return _viewModel.UpcomingSlots[section].Description;
				}
				else
				{
					return string.Empty;
				}
			}
			
			public override int NumberOfSections(UITableView tableView)
			{
				return _viewModel.UpcomingSlots.Count + 1;
			}
		}
	}
}

