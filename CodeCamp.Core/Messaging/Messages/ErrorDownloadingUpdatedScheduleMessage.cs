using System;
using TinyMessenger;

namespace CodeCamp.Core.Messaging.Messages
{
	public class ErrorDownloadingUpdatedScheduleMessage : TinyMessageBase
	{
		public ErrorDownloadingUpdatedScheduleMessage(object sender)
			: base(sender)
		{
		}
	}
}

