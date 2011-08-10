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
									   		 _tracksController,
									   		 _speakersController,
									   		 _campOverviewController;
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			_campOverviewController = new CodeCampNavigationController();
			_campOverviewController.NavigationBar.BarStyle = UIBarStyle.Black;
			_campOverviewController.PushViewController(new CampOverviewViewController(), false);
			_campOverviewController.TabBarItem = new UITabBarItem("Overview", UIImage.FromFile("Content/Images/home1.png"), 0);
			
			_sessionsController = new CodeCampNavigationController();
			_sessionsController.NavigationBar.BarStyle = UIBarStyle.Black;
			_sessionsController.PushViewController(new SessionListViewController(), false);
			_sessionsController.TabBarItem = new UITabBarItem("Schedule", UIImage.FromFile("Content/Images/megaphone.png"), 1);
			
			var tracks = AppDelegate.CodeCampRepository.GetTrackNames();
			_tracksController = new CodeCampNavigationController();
			_tracksController.NavigationBar.BarStyle = UIBarStyle.Black;
			_tracksController.PushViewController(new TrackListViewController(tracks), false);
			_tracksController.TabBarItem = new UITabBarItem("Tracks", UIImage.FromFile("Content/Images/todo-list.png"), 2);
			
			var speakers = AppDelegate.CodeCampRepository.GetSpeakers();
			_speakersController = new CodeCampNavigationController();
			_speakersController.NavigationBar.BarStyle = UIBarStyle.Black;
			_speakersController.PushViewController(new SpeakerListViewController(speakers), false);
			_speakersController.TabBarItem = new UITabBarItem("Speakers", UIImage.FromFile("Content/Images/sing.png"), 3);
			
			ViewControllers = new UIViewController[] {
				_campOverviewController,
				_sessionsController,
				_tracksController,
				_speakersController
			};
			
			SelectedIndex = 0;
		}
	}
}

