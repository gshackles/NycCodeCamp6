using System;
using TinyMessenger;

namespace CodeCamp.Core.Messaging.Messages
{
	public class ErrorCheckingForUpdatedScheduleMessage : TinyMessageBase
	{
		public ErrorCheckingForUpdatedScheduleMessage(object sender)
			: base(sender)
		{
		}
	}
}

