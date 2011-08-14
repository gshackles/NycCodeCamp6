using System;
using TinyMessenger;

namespace CodeCamp.Core.Messaging.Messages
{
	public class StartedCheckingForUpdatedScheduleMessage : TinyMessageBase
	{
		public StartedCheckingForUpdatedScheduleMessage(object sender)
			: base(sender)
		{
		}
	}
}

