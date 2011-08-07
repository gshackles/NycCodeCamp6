using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace NycCodeCamp.MonoTouchApp
{
	public abstract class ListControllerBase : UITableViewController
	{
		public ListControllerBase (UITableViewStyle style = UITableViewStyle.Plain)
			: base(style)
		{
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			TableView.TableFooterView = new UIView(RectangleF.Empty);
		}
	}
}

