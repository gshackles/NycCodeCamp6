using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;
using System.Drawing;

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
			SessionTitle.SizeToFit();
			
			SpeakerName.Frame = new RectangleF(SpeakerName.Frame.X, SessionTitle.Frame.Y + SessionTitle.Frame.Height + 5,
											   SpeakerName.Frame.Width, SpeakerName.Frame.Height);
			SpeakerName.SetTitle(_session.Speaker.Name, UIControlState.Normal);
			SpeakerName.SizeToFit();
			
			SessionTime.Frame = new RectangleF(SessionTime.Frame.X, SpeakerName.Frame.Y + SpeakerName.Frame.Height + 5,
											   SessionTime.Frame.Width, SessionTime.Frame.Height);
			SessionTime.Text = string.Format("{0} - {1}",
											 _session.Starts.ToLocalTime().ToShortTimeString(),
											 _session.Ends.ToLocalTime().ToShortTimeString());
			SessionTime.SizeToFit();
			
			SessionAbstract.Frame = new RectangleF(SessionAbstract.Frame.X, SessionTime.Frame.Y + SessionTime.Frame.Height + 5,
											   	   SessionAbstract.Frame.Width, SessionAbstract.Frame.Height);
			SessionAbstract.Text = _session.Abstract;
			SessionAbstract.SizeToFit();
			
			Scroller.ContentSize = new SizeF(Scroller.Frame.Width, 
											 SessionTitle.Frame.Height
												+ SpeakerName.Frame.Height
												+ SessionTime.Frame.Height
												+ SessionAbstract.Frame.Height);
			Scroller.Frame = new RectangleF(Scroller.Frame.X, 
											Scroller.Frame.Y, 
											Scroller.Frame.Width, 
											Scroller.Superview.Frame.Height 
												- NavigationController.Toolbar.Frame.Height 
												- NavigationController.NavigationBar.Frame.Height);
			
			
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

