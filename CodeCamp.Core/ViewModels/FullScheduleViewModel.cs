using System;
using System.Collections.Generic;
using System.Linq;
using CodeCamp.Core.Entities;

namespace CodeCamp.Core.ViewModels
{
    public class FullScheduleViewModel
    {
        public IList<TimeSlot> Schedule { get; set; }

        public FullScheduleViewModel(IList<Session> allSessions)
        {
            Schedule =
                (
                    from session in allSessions
                    group session by new { session.Starts, session.Ends } into timeSlot
                    orderby timeSlot.Key.Starts
                    select new TimeSlot
                    {
                        Description = string.Format("{0} - {1}",
                                                    timeSlot.Key.Starts.ToLocalTime().ToShortTimeString(),
                                                    timeSlot.Key.Ends.ToLocalTime().ToShortTimeString()),
                        Sessions = timeSlot.OrderBy(session => session.Title).ToList()
                    }
                ).ToList();
        }

        public class TimeSlot
        {
            public string Description { get; set; }
            public IList<Session> Sessions { get; set; }
        }
    }
}
