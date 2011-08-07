using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.IO;
using CodeCamp.Core.DataAccess;

namespace NycCodeCamp.MonoTouchApp
{
	public class Application
	{
		static void Main(string[] args)
		{
			UIApplication.Main (args);
		}
	}
	
	public partial class AppDelegate : UIApplicationDelegate
	{
		private UITabBarController _tabController;
		
		public static ICodeCampRepository CodeCampRepository { get; private set; }
		
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			CodeCampRepository = new XmlCodeCampRepository(getCodeCampXml());
			
			_tabController = new TabController();
			window.AddSubview(_tabController.View);
			
			window.MakeKeyAndVisible ();
	
			return true;
		}
	
		private string getCodeCampXml() 
		{
			using (var reader = new StreamReader("Content/CodeCamp.xml"))
			{
				return reader.ReadToEnd();
			}
		}
		
		// This method is required in iPhoneOS 3.0
		public override void OnActivated(UIApplication application)
		{
		}
	}
}

