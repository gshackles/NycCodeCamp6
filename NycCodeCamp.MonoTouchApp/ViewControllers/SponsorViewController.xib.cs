using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;
using System.Drawing;

namespace NycCodeCamp.MonoTouchApp
{
	public partial class SponsorViewController : DetailViewControllerBase
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
		}
		
		void Initialize()
		{
		}
		
		#endregion
		
		private readonly Sponsor _sponsor;

		private List<UIView> _views = null;
		protected override List<UIView> Views 
		{
			get 
			{
				if (_views == null)
				{
					_views = new List<UIView>() { SponsorName, Description };
				}
				
				return _views;
			}
		}
		
		protected override MonoTouch.UIKit.UIScrollView ScrollView 
		{
			get { return Scroller; }
		}
		
		protected override bool ToolbarVisible 
		{
			get { return true; }
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
		
			SponsorName.Text = _sponsor.Name;
			Description.Text = _sponsor.Description;
			
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
		}
		
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear (animated);
			
			NavigationController.SetToolbarHidden(true, true);
		}
	}
}

