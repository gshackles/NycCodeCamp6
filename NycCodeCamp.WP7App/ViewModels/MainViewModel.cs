using System;
using System.Collections.Generic;
using System.Linq;
using CodeCamp.Core.Entities;
using CodeCamp.Core.ViewModels;
using NycCodeCamp.WP7App.Entities;

namespace NycCodeCamp.WP7App.ViewModels
{
    public class MainViewModel
    {
        public CampOverviewViewModel Overview { get; set; }
        public FullScheduleViewModel FullSchedule { get; set; }
        public IList<Speaker> Speakers { get; set; }
        public SponsorListViewModel Sponsors { get; set; }
        public IList<string> Tags { get; set; }
        public IList<Room> Rooms { get; set; }

        public MainViewModel()
        {
            var allSessions = App.CodeCampService.Repository.GetSessions();

            Overview = new CampOverviewViewModel(allSessions);

            FullSchedule = new FullScheduleViewModel(allSessions);

            Speakers = 
                App.CodeCampService.Repository.GetSpeakers()
                    .OrderBy(speaker => speaker.Name)
                    .ToList();

            var sponsors = App.CodeCampService.Repository.GetSponsors();
            var tiers = App.CodeCampService.Repository.GetSponsorTiers();
            Sponsors = new SponsorListViewModel(tiers, sponsors);

            Tags =
                App.CodeCampService.Repository.GetTags()
                    .OrderBy(tag => tag)
                    .ToList();

            Rooms = new List<Room>
			{
				new Room("Lower Level", "Auditorium, Cafeteria", "GameOfThrones.jpg"),
				new Room("Ground Level", "Student Union meeting spaces", "GameOfThrones.jpg"),
				new Room("Second Floor", "Classroom meeting spaces", "GameOfThrones.jpg")
			};
        }
    }
}
