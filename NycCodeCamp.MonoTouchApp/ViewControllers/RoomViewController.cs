using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace NycCodeCamp.MonoTouchApp
{
	public class RoomViewController : UIViewController
	{
		private UIScrollView _scroller;
		
		public RoomViewController ()
		{
			HidesBottomBarWhenPushed = true;
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			NavigationItem.Title = "Room Layout";
			
			_scroller = new UIScrollView(View.Frame);
			var roomImage = new UIImageView(UIImage.FromFile("Content/Images/GameOfThrones.jpg"));
			
			_scroller.AddSubview(roomImage);
			_scroller.ContentSize = roomImage.Frame.Size;
			_scroller.MinimumZoomScale = 1.0f;
			_scroller.MaximumZoomScale = 3.0f;
			_scroller.MultipleTouchEnabled = true;
			_scroller.ViewForZoomingInScrollView = (scrollView) => roomImage;
			
			View.AddSubview(_scroller);
		}
		
		public override void WillAnimateRotation(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillAnimateRotation (toInterfaceOrientation, duration);
			
			_scroller.Frame = 
				new RectangleF(0, 0, 
							   View.Frame.Width, 
						   	   View.Frame.Height);
		}
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);
			
			_scroller.Frame = 
				new RectangleF(0, 0, 
							   View.Frame.Width, 
						   	   View.Frame.Height);
		}
	}
}

