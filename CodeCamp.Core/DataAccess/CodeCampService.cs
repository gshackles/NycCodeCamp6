using System;
using CodeCamp.Core.DataAccess;
using System.IO;

namespace CodeCamp.Core
{
	public class CodeCampService
	{
		public CodeCampRepository Repository { get; private set; }
		
		private readonly CodeCampDataClient _client;
		private readonly string _xmlPath;
		
		public CodeCampService(string xmlPath, string baseUrl, string campKey)
		{
			Repository = new CodeCampRepository(null);
			_client = new CodeCampDataClient(baseUrl, campKey);
			_xmlPath = xmlPath;
			
			if (!File.Exists(xmlPath))
			{
				downloadNewXmlFile();
			}
			else
			{
				using (var reader = new StreamReader(_xmlPath))
				{
					Repository = new CodeCampRepository(reader.ReadToEnd());
				}
			}
		}
		
		public void CheckForUpdatedSchedule()
		{
			MessageHub.Instance.Publish(new StartedCheckingForUpdatedScheduleMessage(this));
			
			_client.GetCurrentVersion(version =>
			{
				if (version < 0)
				{
					MessageHub.Instance.Publish(new ErrorCheckingForUpdatedScheduleMessage(this));
				}
				else if (version > Repository.CurrentVersion)
				{
					MessageHub.Instance.Publish(new FoundUpdatedScheduleMessage(this));
					
					downloadNewXmlFile();
				}
				else
				{
					MessageHub.Instance.Publish(new NoUpdatedScheduleAvailableMessage(this));
				}
			});
		}
		
		private void downloadNewXmlFile()
		{
			MessageHub.Instance.Publish(new StartedDownloadingUpdatedScheduleMessage(this));
			
			_client.GetXml(xml =>
			{
				if (string.IsNullOrEmpty(xml))
				{
					MessageHub.Instance.Publish(new ErrorDownloadingUpdatedScheduleMessage(this));
				}
				else
				{
					using (var writer = new StreamWriter(_xmlPath))
					{
						writer.Write(xml);
					}
					
					Repository = new CodeCampRepository(xml);
					
					MessageHub.Instance.Publish(new FinishedUpdatingScheduleMessage(this));
				}
			});
		}
	}
}

