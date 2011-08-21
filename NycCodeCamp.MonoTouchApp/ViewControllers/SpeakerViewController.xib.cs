using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;
using MonoTouch.MessageUI;
using System.Drawing;

namespace NycCodeCamp.MonoTouchApp
{
	public partial class SpeakerViewController : DetailViewControllerBase
	{
		#region Constructors
		
		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code
		
		public SpeakerViewController(IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		[Export ("initWithCoder:")]
		public SpeakerViewController(NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		public SpeakerViewController(Speaker speaker) : base ("SpeakerViewController", null)
		{
			Initialize ();
			
			_speaker = speaker;
		}
		
		void Initialize()
		{
		}
		
		#endregion
		
		private Speaker _speaker;
		private MFMailComposeViewController _mailController;
		
		protected override MonoTouch.UIKit.UIScrollView ScrollView 
		{
			get { return Scroller; }
		}
		
		private List<UIView> _views = null;
		protected override List<UIView> Views 
		{
			get 
			{
				if (_views == null)
				{
					_views = new List<UIView>() { SpeakerName, SpeakerBio };
				}
				
				return _views;
			}
		}
		
		protected override bool ToolbarVisible 
		{
			get { return true; }
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
		
			SpeakerName.Text = _speaker.Name;
			SpeakerBio.Text = _speaker.Bio;
			
			var toolbarButtons = new List<UIBarButtonItem>();
			
			if (!string.IsNullOrEmpty(_speaker.Email))
			{
				var emailButton = 
					new UIBarButtonItem(UIImage.FromFile("Content/Images/email.png"), 
										UIBarButtonItemStyle.Plain, new EventHandler(sendEmail));
				
				emailButton.Enabled = MFMailComposeViewController.CanSendMail;
				
				toolbarButtons.Add(emailButton);
			}
			
			if (!string.IsNullOrEmpty(_speaker.Website))
			{
				var url = new NSUrl(_speaker.Website);
				
				var websiteButton =
					new UIBarButtonItem(UIImage.FromFile("Content/Images/globe.png"),
										UIBarButtonItemStyle.Plain, 
										(s, e) => UIApplication.SharedApplication.OpenUrl(url));
				
				websiteButton.Enabled = UIApplication.SharedApplication.CanOpenUrl(url);
				
				toolbarButtons.Add(websiteButton);
			}
			
			ToolbarItems = toolbarButtons.ToArray();
			NavigationController.Toolbar.BarStyle = UIBarStyle.Black;
			NavigationController.Toolbar.Translucent = true;
		}
		
		private void sendEmail(object sender, EventArgs args) 
		{
			_mailController = new MFMailComposeViewController();
			_mailController.NavigationBar.TintColor = UIColor.Black;
			_mailController.SetToRecipients(new string[] { _speaker.Email });
			_mailController.Finished += delegate(object mailSender, MFComposeResultEventArgs mailArgs) {
				if (mailArgs.Result == MFMailComposeResult.Failed)
				{
					new UIAlertView("Send Mail Failure", "Unable to send email, please try again later",
									null, "Ok", null).Show();
				}
				
				NavigationController.DismissModalViewControllerAnimated(true);
			};

			NavigationController.PresentModalViewController(_mailController, true);
		}
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);
			
			NavigationController.SetToolbarHidden(ToolbarItems.Count() == 0, true);
		}
		
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear (animated);
			
			NavigationController.SetToolbarHidden(true, true);
		}
	}
}

