using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CodeCamp.Core;
using CodeCamp.Core.DataAccess;
using CodeCamp.Core.Messaging;
using CodeCamp.Core.Messaging.Messages;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NycCodeCamp.MonoTouchApp
{
	public class Application
	{
		static void Main(string[] args)
		{
			UIApplication.Main (args);
		}
	}
	
	public partial class AppDelegate : UIApplicationDelegate
	{
		private TabController _tabController;
		private WaitingView _waitingView;
		
		public static CodeCampService CodeCampService { get; private set; }
		
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			_waitingView = new WaitingView();
			
			subscribeToMessages();
			
			//CodeCampService = new CodeCampService("http://localhost:8080/v1", "sample");
			CodeCampService = new CodeCampService("http://codecamps.gregshackles.com/v1", "sample");
			
			_tabController = new TabController();
			_tabController.View.BackgroundColor = UIColor.Clear;
			
			var backgroundImage = new UIImageView(UIImage.FromFile("Content/Images/background.png"));
			backgroundImage.UserInteractionEnabled = true;
			backgroundImage.Frame = new System.Drawing.RectangleF(0, 0, window.Frame.Width, window.Frame.Height);
			backgroundImage.AddSubview(_tabController.View);
			
			window.AddSubview(backgroundImage);
			
			window.MakeKeyAndVisible ();
	
			return true;
		}
		
		private void subscribeToMessages()
		{
			MessageHub.Instance.Subscribe<StartedCheckingForUpdatedScheduleMessage>(msg =>
				InvokeOnMainThread(() => _waitingView.Show("Checking for updated schedule")));
			
			MessageHub.Instance.Subscribe<ErrorCheckingForUpdatedScheduleMessage>(msg =>
			{
				InvokeOnMainThread(() =>
				{
					_waitingView.Hide();
					
					new UIAlertView("Error", "Unable to check for an updated schedule, please try again later",
									null, "Ok", null).Show();
				});
			});
			
			MessageHub.Instance.Subscribe<NoUpdatedScheduleAvailableMessage>(msg =>
			{	
				InvokeOnMainThread(() => 
				{
					_waitingView.Hide();
				
					new UIAlertView("", "Your schedule is already up to date",
									null, "Ok", null).Show();
				});
			});
			
			MessageHub.Instance.Subscribe<FoundUpdatedScheduleMessage>(msg =>
				InvokeOnMainThread(() => _waitingView.Hide()));
			
			MessageHub.Instance.Subscribe<StartedDownloadingUpdatedScheduleMessage>(msg =>
				InvokeOnMainThread(() => _waitingView.Show("Downloading updated event details")));
			
			MessageHub.Instance.Subscribe<ErrorDownloadingUpdatedScheduleMessage>(msg =>
			{
				InvokeOnMainThread(() =>
				{
					_waitingView.Hide();
					
					new UIAlertView("Error", "Unable to download the event details, please try again later",
									null, "Ok", null).Show();
				});
			});
			
			MessageHub.Instance.Subscribe<FinishedUpdatingScheduleMessage>(msg =>
			{
				InvokeOnMainThread(() =>
				{
					_tabController.ReloadViews();
					
					_waitingView.Hide();
				});
			});
		}
		
		// This method is required in iPhoneOS 3.0
		public override void OnActivated(UIApplication application)
		{
		}
	}
}

