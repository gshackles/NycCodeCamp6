using System;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;
using System.Collections.Generic;
using CodeCamp.Core.DataAccess;

namespace NycCodeCamp.MonoTouchApp
{
	public class TabController : UITabBarController
	{
		private CodeCampNavigationController _sessionsController,
									   		 _tagsController,
									   		 _speakersController,
									   		 _campOverviewController,
											 _sponsorsController,
											 _roomController;
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			ReloadViews();
		}
		
		public void ReloadViews()
		{
			if (_campOverviewController != null)
				_campOverviewController.Dispose();
			if (_sessionsController != null)
				_sessionsController.Dispose();
			if (_tagsController != null)
				_tagsController.Dispose();
			if (_speakersController != null)
				_speakersController.Dispose();
			if (_sponsorsController != null)
				_sponsorsController.Dispose();
			if (_roomController != null)
				_roomController.Dispose();
			
			_campOverviewController = new CodeCampNavigationController();
			_campOverviewController.NavigationBar.BarStyle = UIBarStyle.Black;
			_campOverviewController.PushViewController(new CampOverviewViewController(), false);
			_campOverviewController.TabBarItem = new UITabBarItem("Overview", UIImage.FromFile("Content/Images/home1.png"), 0);
			
			_sessionsController = new CodeCampNavigationController();
			_sessionsController.NavigationBar.BarStyle = UIBarStyle.Black;
			_sessionsController.PushViewController(new SessionListViewController(), false);
			_sessionsController.TabBarItem = new UITabBarItem("Schedule", UIImage.FromFile("Content/Images/megaphone.png"), 1);
			
			var tags = AppDelegate.CodeCampService.Repository.GetTags();
			_tagsController = new CodeCampNavigationController();
			_tagsController.NavigationBar.BarStyle = UIBarStyle.Black;
			_tagsController.PushViewController(new TagListViewController(tags), false);
			_tagsController.TabBarItem = new UITabBarItem("Tags", UIImage.FromFile("Content/Images/todo-list.png"), 2);
			
			var speakers = AppDelegate.CodeCampService.Repository.GetSpeakers();
			_speakersController = new CodeCampNavigationController();
			_speakersController.NavigationBar.BarStyle = UIBarStyle.Black;
			_speakersController.PushViewController(new SpeakerListViewController(speakers), false);
			_speakersController.TabBarItem = new UITabBarItem("Speakers", UIImage.FromFile("Content/Images/sing.png"), 3);
			
			var sponsors = AppDelegate.CodeCampService.Repository.GetSponsors();
			var tiers = AppDelegate.CodeCampService.Repository.GetSponsorTiers();
			_sponsorsController = new CodeCampNavigationController();
			_sponsorsController.NavigationBar.BarStyle = UIBarStyle.Black;
			_sponsorsController.PushViewController(new SponsorListViewController(sponsors, tiers), false);
			_sponsorsController.TabBarItem = new UITabBarItem("Sponsors", UIImage.FromFile("Content/Images/star.png"), 4);
			
			_roomController = new CodeCampNavigationController();
			_roomController.NavigationBar.BarStyle = UIBarStyle.Black;
			_roomController.PushViewController(new RoomListViewController(), false);
			_roomController.TabBarItem = new UITabBarItem("Rooms", UIImage.FromFile("Content/Images/navigate-signs2.png"), 5);
			
			ViewControllers = new UIViewController[] {
				_campOverviewController,
				_sessionsController,
				_tagsController,
				_speakersController,
				_sponsorsController,
				_roomController
			};
			
			MoreNavigationController.NavigationBar.BarStyle = UIBarStyle.Black;

			SelectedIndex = 0;
			
			OnCustomizingViewControllers += delegate(object sender, UITabBarCustomizeEventArgs e) 
			{
				var editView = View.Subviews[1];
				var modalNavBar = (UINavigationBar)editView.Subviews[0];
				modalNavBar.TintColor = UIColor.Black;
				modalNavBar.TopItem.Title = "Edit Tabs";
			};
		}
	}
}