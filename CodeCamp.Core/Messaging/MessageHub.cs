using System;
using TinyMessenger;

namespace CodeCamp.Core.Messaging
{
	public class MessageHub
    {
        private static readonly TinyMessengerHub _instance = new TinyMessengerHub();

	    private MessageHub() { }

        public static TinyMessengerHub Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}

