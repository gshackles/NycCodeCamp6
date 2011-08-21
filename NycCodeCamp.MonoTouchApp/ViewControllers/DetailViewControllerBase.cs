using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NycCodeCamp.MonoTouchApp
{
	public abstract class DetailViewControllerBase : UIViewController
	{
		private readonly int _margin = 20;
		
		public DetailViewControllerBase() : base() 
		{
			HidesBottomBarWhenPushed = true;
		}
		
		public DetailViewControllerBase(NSCoder coder) : base(coder) 
		{
			HidesBottomBarWhenPushed = true;
		}
		
		public DetailViewControllerBase(MonoTouch.Foundation.NSObjectFlag t) : base(t) 
		{
			HidesBottomBarWhenPushed = true;
		}
		
		public DetailViewControllerBase(IntPtr handle) : base(handle) 
		{
			HidesBottomBarWhenPushed = true;
		}
		
		public DetailViewControllerBase(string nibName, NSBundle bundle) : base(nibName, bundle) 
		{
			HidesBottomBarWhenPushed = true;
		}
		
		protected abstract UIScrollView ScrollView { get; }
		protected abstract List<UIView> Views { get; }
		
		protected virtual bool ToolbarVisible { get { return false; } }
		
		private float ElementWidth
		{
			get { return View.Frame.Width - (2 * _margin); }
		}
		
		public override void WillAnimateRotation(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillAnimateRotation(toInterfaceOrientation, duration);
			
			layoutElements();
		}
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);
			
			if (ToolbarVisible)
			{
				ScrollView.Frame = 
					new RectangleF(ScrollView.Frame.X, ScrollView.Frame.Y, ScrollView.Frame.Width, 
								   ScrollView.Frame.Height - NavigationController.Toolbar.Frame.Height);
			}
			
			layoutElements();
		}
		
		private void layoutElements()
		{
			if (Views.Count == 0)
				return;
			
			var topElement = Views.First();
			
			topElement.Frame = new RectangleF(_margin, 0, ElementWidth, 0);
			topElement.SizeToFit();
			
			for (int i = 1; i < Views.Count; i++)
			{
				layoutElementBelow(Views[i - 1], Views[i], 5);
			}
			
			var bottomFrame = Views.Last().Frame;
			ScrollView.ContentSize = new SizeF(ElementWidth, bottomFrame.Y + bottomFrame.Height);
		}
		
		private void layoutElementBelow(UIView topElement, UIView bottomElement, int padding)
		{
			bottomElement.Frame = new RectangleF(_margin, topElement.Frame.Y + topElement.Frame.Height + padding,
												 ElementWidth, 0);
			bottomElement.SizeToFit();
		}
	}
}

