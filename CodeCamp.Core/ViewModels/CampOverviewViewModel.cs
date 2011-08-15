using System;
using System.Collections.Generic;
using System.Linq;
using CodeCamp.Core.Entities;

namespace CodeCamp.Core.ViewModels
{
	public class CampOverviewViewModel
	{
		public IList<TimeSlot> UpcomingSlots { get; set; }
		
		public CampOverviewViewModel(IList<Session> allSessions)
		{
			// grab the first two time slots that haven't ended yet
			UpcomingSlots =
				allSessions
					.ToList()
					.Where(session => session.Ends > DateTime.UtcNow)
					.Select(session => session.Starts)
					.Distinct()
					.Select(time => new TimeSlot
									{
										Starts = time,
										Description = getDescriptionForTime(time),
										Sessions = allSessions
													.Where(session => session.Starts == time)
													.OrderBy(session => session.Title)
													.ToList()
									})
					.OrderBy(slot => slot.Starts)
					.Take(2)
					.ToList();
		}
		
		private string getDescriptionForTime(DateTime startTime)
		{
			if (startTime < DateTime.UtcNow)
				return "On Now";
			
			return "Starting at " + startTime.ToLocalTime().ToShortTimeString();
		}
		
		public class TimeSlot
		{
			public DateTime Starts { get; set; }
			public string Description { get; set; }
			public IList<Session> Sessions { get; set; }
		}
	}
}

