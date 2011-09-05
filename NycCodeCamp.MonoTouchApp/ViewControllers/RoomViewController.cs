using System;
using MonoTouch.UIKit;
using System.Drawing;
using CodeCamp.Core.Entities;

namespace NycCodeCamp.MonoTouchApp
{
	public class RoomViewController : UIViewController
	{
		private UIScrollView _scroller;
		private readonly Room _room;
		
		public RoomViewController (string roomKey)
		{
			HidesBottomBarWhenPushed = true;
			_room = AppDelegate.CodeCampService.Repository.GetRoom(roomKey);
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			NavigationItem.Title = _room.Name;
			
			_scroller = new UIScrollView(View.Frame);
			var roomImage = new UIImageView(UIImage.FromFile("Content/Images/Maps/" + _room.Key + ".png"));
			
			_scroller.AddSubview(roomImage);
			_scroller.ContentSize = roomImage.Frame.Size;
			_scroller.MinimumZoomScale = 0.50f;
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

