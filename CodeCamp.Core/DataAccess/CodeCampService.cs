using System;
using System.IO;
using CodeCamp.Core.Messaging;
using CodeCamp.Core.Messaging.Messages;

namespace CodeCamp.Core.DataAccess
{
	public class CodeCampService
	{
		public CodeCampRepository Repository { get; private set; }
		
		private readonly CodeCampDataClient _client;
	    private readonly IFileSystemHelper _fileHelper;
	    private readonly string _fileName;
		
		public CodeCampService(string baseUrl, string campKey)
		{
#if WINDOWS_PHONE
            _fileHelper = new IsolatedStorageFileSystemHelper();
#else
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
											 "../Library/Caches");
            _fileHelper = new StandardFileSystemHelper(folderPath);
#endif

            _fileName = campKey + ".xml";
			Repository = new CodeCampRepository(null);
			_client = new CodeCampDataClient(baseUrl, campKey);

            if (!_fileHelper.FileExists(_fileName))
            {
                downloadNewXmlFile();
            }
			else
			{
                Repository = new CodeCampRepository(_fileHelper.ReadFile(_fileName));
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
				    _fileHelper.WriteFile(_fileName, xml);

                    Repository = new CodeCampRepository(xml);
					
					MessageHub.Instance.Publish(new FinishedUpdatingScheduleMessage(this));
				}
			});
		}
	}
}

