using System;
using TinyMessenger;

namespace CodeCamp.Core.Messaging.Messages
{
	public class NoUpdatedScheduleAvailableMessage : TinyMessageBase
	{
		public NoUpdatedScheduleAvailableMessage(object sender)
			: base(sender)
		{
		}
	}
}

