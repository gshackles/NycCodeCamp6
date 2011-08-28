using System;
using System.Collections.Generic;
using CodeCamp.Core.Entities;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NycCodeCamp.MonoTouchApp
{
	public partial class SessionViewController : DetailViewControllerBase
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
		}
		
		void Initialize()
		{
		}
		
		#endregion
	
		private Session _session;
		
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
					_views = new List<UIView>() { SessionTitle, SpeakerName, SessionTime, SessionRoom, SessionAbstract };
				}
				
				return _views;
			}
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			SessionTitle.Text = _session.Title;
			SpeakerName.SetTitle(_session.Speaker.Name, UIControlState.Normal);
			SessionTime.Text = string.Format("{0} - {1}",
											 _session.Starts.ToLocalTime().ToShortTimeString(),
											 _session.Ends.ToLocalTime().ToShortTimeString());
			SessionRoom.SetTitle("Room: " + _session.Room, UIControlState.Normal);
			SessionAbstract.Text = _session.Abstract;
			
			SpeakerName.TouchUpInside += delegate 
			{
				NavigationController.PushViewController(
					new SpeakerViewController(_session.Speaker), true);
			};
			
			SessionRoom.TouchUpInside += delegate 
			{
				NavigationController.PushViewController(
					new RoomViewController(), true);
			};
		}
	}
}

