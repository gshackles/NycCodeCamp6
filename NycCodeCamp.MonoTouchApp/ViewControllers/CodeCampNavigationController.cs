using System;
using MonoTouch.UIKit;

namespace NycCodeCamp.MonoTouchApp
{
	public class CodeCampNavigationController : UINavigationController
	{
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

