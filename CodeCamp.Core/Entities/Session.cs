using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeCamp.Core.Entities
{
    public class Session
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
		public string Room { get; set; }
        public Speaker Speaker { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
		public IList<string> Tags { get; set; }
    }
}
