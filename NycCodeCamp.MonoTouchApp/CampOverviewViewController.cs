using System;
using System.Linq;
using CodeCamp.Core.Entities;
using System.Collections.Generic;
using MonoTouch.UIKit;

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
			
			var allSessions = AppDelegate.CodeCampRepository.GetSessions();
			
			NavigationItem.Title = "NYC Code Camp";
			TableView.Source = new OverviewTableViewSource(this, allSessions);
		}
		
		private class OverviewTableViewSource : UITableViewSource
		{
			private CampOverviewViewController _hostController;
			private Dictionary<DateTime, List<Session>> _upcomingSlots;
			private const string OVERVIEW_SCHEDULE_CELL = "overviewScheduleCell";
			private const string OVERVIEW_LINK_CELL = "overviewLinkCell";
			
			public OverviewTableViewSource (CampOverviewViewController hostController, IList<Session> allSessions)
			{
				_hostController = hostController;
				
				// grab the first two time slots that haven't ended yet
				_upcomingSlots =
					allSessions
						.Where(session => session.Ends > DateTime.UtcNow)
						.Select(session => session.Starts)
						.Distinct()
						.OrderBy(time => time)
						.Take(2)
						.ToDictionary(time => time, 
									  time => allSessions
												.Where(session => session.Starts == time)
												.OrderBy(session => session.Title)
												.ToList());
			}
			
			public override int RowsInSection(UITableView tableview, int section)
			{
				return section < _upcomingSlots.Keys.Count
						? _upcomingSlots[_upcomingSlots.Keys.ElementAt(section)].Count
						: 1;
			}

			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				if (indexPath.Section < _upcomingSlots.Keys.Count)
				{
					var cell = tableView.DequeueReusableCell(OVERVIEW_SCHEDULE_CELL)
								?? new UITableViewCell(UITableViewCellStyle.Subtitle, OVERVIEW_SCHEDULE_CELL);
					var session = _upcomingSlots[_upcomingSlots.Keys.ElementAt(indexPath.Section)][indexPath.Row];
					
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
				if (indexPath.Section < _upcomingSlots.Keys.Count)
				{
					var selectedSession = _upcomingSlots[_upcomingSlots.Keys.ElementAt(indexPath.Section)].ElementAt(indexPath.Row);
					
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
				return indexPath.Section < _upcomingSlots.Keys.Count
						? 65
						: 55;
			}
			
			public override string TitleForHeader(UITableView tableView, int section)
			{
				if (section < _upcomingSlots.Keys.Count)
				{
					var slotTime = _upcomingSlots.Keys.ElementAt(section);
					
					if (slotTime < DateTime.UtcNow)
						return "On Now";
					else
						return "Starting at " + slotTime.ToLocalTime().ToShortTimeString();
				}
				else
				{
					return string.Empty;
				}
			}
			
			public override int NumberOfSections(UITableView tableView)
			{
				return _upcomingSlots.Keys.Count + 1;
			}
		}
	}
}

