using System;
using System.Net;

namespace CodeCamp.Core
{
	public class CodeCampDataClient
	{
		private readonly string _baseUrl;
		
		public CodeCampDataClient(string baseUrl, string campKey)
		{
			_baseUrl = string.Format("{0}/{1}", baseUrl, campKey);
		}
		
		public void GetCurrentVersion(Action<int> callback) 
		{
			var client = new WebClient();
			
			client.DownloadStringCompleted += delegate(object sender, DownloadStringCompletedEventArgs e) {
				int version = e.Error != null
								? -1
								: int.Parse(e.Result);
				
				callback(version);
			};
			
			client.DownloadStringAsync(
				new Uri(_baseUrl + "/Version"));
		}
		
		public void GetXml(Action<string> callback)
		{
			var client = new WebClient();
			
			client.DownloadStringCompleted += delegate(object sender, DownloadStringCompletedEventArgs e) {
				string xml = e.Error != null
								? null
								: e.Result;
				
				callback(xml);
			};
			
			client.DownloadStringAsync(
				new Uri(_baseUrl + "/Xml"));
		}
	}
}