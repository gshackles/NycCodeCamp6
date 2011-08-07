using System;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;
using System.Collections.Generic;
using CodeCamp.Core.DataAccess;

namespace NycCodeCamp.MonoTouchApp
{
	public class TabController : UITabBarController
	{
		private UINavigationController _sessionsController,
									   _tracksController,
									   _speakersController;
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			_sessionsController = new UINavigationController();
			_sessionsController.NavigationBar.BarStyle = UIBarStyle.Black;
			_sessionsController.TabBarItem = new UITabBarItem("Sessions", UIImage.FromFile("Content/Images/megaphone.png"), 0);
			_sessionsController.PushViewController(new SessionListViewController(), false);
			
			var tracks = AppDelegate.CodeCampRepository.GetTrackNames();
			_tracksController = new UINavigationController();
			_tracksController.NavigationBar.BarStyle = UIBarStyle.Black;
			_tracksController.TabBarItem = new UITabBarItem("Tracks", UIImage.FromFile("Content/Images/todo-list.png"), 1);
			_tracksController.PushViewController(new TrackListViewController(tracks), false);
			
			var speakers = AppDelegate.CodeCampRepository.GetSpeakers();
			_speakersController = new UINavigationController();
			_speakersController.NavigationBar.BarStyle = UIBarStyle.Black;
			_speakersController.TabBarItem = new UITabBarItem("Speakers", UIImage.FromFile("Content/Images/sing.png"), 2);
			_speakersController.PushViewController(new SpeakerListViewController(speakers), false);
			
			ViewControllers = new UIViewController[] {
				_sessionsController,
				_tracksController,
				_speakersController
			};
			SelectedIndex = 0;
		}
	}
}

