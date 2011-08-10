using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;

namespace NycCodeCamp.MonoTouchApp
{
	public partial class SessionViewController : UIViewController
	{
		#region Constructors
		
		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code
		
		public SessionViewController(IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		[Export ("initWithCoder:")]
		public SessionViewController(NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		public SessionViewController(Session session) 
			: base ("SessionViewController", null)
		{
			Initialize ();
			
			_session = session;
			HidesBottomBarWhenPushed = true;
		}
		
		void Initialize()
		{
		}
		
		#endregion
	
		private Session _session;
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			SessionTitle.Text = _session.Title;
			SpeakerName.SetTitle(_session.Speaker.Name, UIControlState.Normal);
			SessionTime.Text = string.Format("{0} - {1}",
											 _session.Starts.ToLocalTime().ToShortTimeString(),
											 _session.Ends.ToLocalTime().ToShortTimeString());
			SessionAbstract.Text = _session.Abstract;
			
			SpeakerName.TouchUpInside += delegate 
			{
				NavigationController.PushViewController(
					new SpeakerViewController(_session.Speaker), true);
			};
		}
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);
			
			TabBarController.TabBar.Hidden = true;
		}
		
		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear (animated);
			
			TabBarController.TabBar.Hidden = false;
		}
	}
}

