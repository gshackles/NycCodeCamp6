using System;
using System.Collections.Generic;
using System.Linq;
using CodeCamp.Core.Entities;
using CodeCamp.Core.ViewModels;

namespace NycCodeCamp.WP7App.ViewModels
{
    public class MainViewModel
    {
        public CampOverviewViewModel Overview { get; set; }
        public FullScheduleViewModel FullSchedule { get; set; }
        public IList<Speaker> Speakers { get; set; }
        public IList<Sponsor> Sponsors { get; set; }
        public IList<string> Tags { get; set; }

        public MainViewModel()
        {
            var allSessions = App.CodeCampService.Repository.GetSessions();

            Overview = new CampOverviewViewModel(allSessions);

            FullSchedule = new FullScheduleViewModel(allSessions);

            Speakers = 
                App.CodeCampService.Repository.GetSpeakers()
                    .OrderBy(speaker => speaker.Name)
                    .ToList();

            Sponsors =
                App.CodeCampService.Repository.GetSponsors()
                    .OrderBy(sponsor => sponsor.Name)
                    .ToList();

            Tags =
                App.CodeCampService.Repository.GetTags()
                    .OrderBy(tag => tag)
                    .ToList();
        }
    }
}
