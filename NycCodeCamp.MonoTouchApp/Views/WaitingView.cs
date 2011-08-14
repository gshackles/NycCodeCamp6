using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace NycCodeCamp.MonoTouchApp
{
	public class WaitingView : UIAlertView
	{
	    private UIActivityIndicatorView _activityView;
	
	    public void Show(string title)
	    {
	    	Title = title;
	    	Show();
	
	    	_activityView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
	    	_activityView.Frame = new RectangleF((Bounds.Width / 2) - 15, Bounds.Height - 50, 30, 30);
	    	_activityView.StartAnimating();
	    	AddSubview(_activityView);
	    }
		
		public void Hide()
		{
			DismissWithClickedButtonIndex(0, true);
		}
	}
}

