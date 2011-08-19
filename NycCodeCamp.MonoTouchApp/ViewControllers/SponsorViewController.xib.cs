using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;
using System.Drawing;

namespace NycCodeCamp.MonoTouchApp
{
	public partial class SponsorViewController : UIViewController
	{
		#region Constructors
		
		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code
		
		public SponsorViewController(IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		[Export ("initWithCoder:")]
		public SponsorViewController(NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		public SponsorViewController(Sponsor sponsor) : base ("SponsorViewController", null)
		{
			Initialize ();
			
			_sponsor = sponsor;
			HidesBottomBarWhenPushed = true;
		}
		
		void Initialize()
		{
		}
		
		#endregion
		
		private readonly Sponsor _sponsor;
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
		
			SponsorName.Text = _sponsor.Name;
			SponsorName.SizeToFit();
			
			Description.Frame = new RectangleF(Description.Frame.X, SponsorName.Frame.Y + SponsorName.Frame.Height + 5,
										  	   Description.Frame.Width, Description.Frame.Height);
			Description.Text = _sponsor.Description;
			Description.SizeToFit();
			
			Scroller.ContentSize = new SizeF(Scroller.Frame.Width, 
											 SponsorName.Frame.Height + Description.Frame.Height);
			Scroller.Frame = new RectangleF(Scroller.Frame.X, 
											Scroller.Frame.Y, 
											Scroller.Frame.Width, 
											Scroller.Superview.Frame.Height 
												- NavigationController.Toolbar.Frame.Height 
												- NavigationController.NavigationBar.Frame.Height);
			
			var toolbarButtons = new List<UIBarButtonItem>();
			
			if (!string.IsNullOrEmpty(_sponsor.Website))
			{
				var url = new NSUrl(_sponsor.Website);
				
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
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);
			
			NavigationController.SetToolbarHidden(ToolbarItems.Count() == 0, true);
			TabBarController.TabBar.Hidden = true;
		}
		
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear (animated);
			
			NavigationController.SetToolbarHidden(true, true);
			TabBarController.TabBar.Hidden = false;
		}
	}
}

