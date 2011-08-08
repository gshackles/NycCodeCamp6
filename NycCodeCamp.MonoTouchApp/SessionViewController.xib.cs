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
			SpeakerName.Text = _session.Speaker.Name;
			SessionTime.Text = _session.Starts.ToLocalTime().ToShortTimeString();
			SessionAbstract.Text = _session.Abstract;
		}
	}
}

