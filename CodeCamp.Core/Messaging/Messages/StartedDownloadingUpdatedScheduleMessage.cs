using System;
using TinyMessenger;

namespace CodeCamp.Core.Messaging.Messages
{
	public class StartedDownloadingUpdatedScheduleMessage : TinyMessageBase
	{
		public StartedDownloadingUpdatedScheduleMessage(object sender)
			: base(sender)
		{
		}
	}
}

