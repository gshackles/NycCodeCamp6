using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CodeCamp.Core.Entities;

namespace NycCodeCamp.MonoTouchApp
{
	public partial class SpeakerViewController : UIViewController
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
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			SpeakerName.Text = _speaker.Name;
			SpeakerBio.Text = _speaker.Bio;
		}
	}
}

