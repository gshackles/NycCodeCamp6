using System;
using TinyMessenger;

namespace CodeCamp.Core.Messaging.Messages
{
	public class FinishedUpdatingScheduleMessage : TinyMessageBase
	{
		public FinishedUpdatingScheduleMessage(object sender)
			: base(sender)
		{
		}
	}
}

