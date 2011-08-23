using System;
using System.Linq;
using System.Collections.Generic;
using CodeCamp.Core.Entities;

namespace NycCodeCamp.WP7App.ViewModels
{
    public class SessionsByTagViewModel
    {
        public string TagName { get; set; }
        public IList<Session> Sessions { get; set; }

        public SessionsByTagViewModel(string tag)
        {
            TagName = tag;
            Sessions =
                App.CodeCampService.Repository.GetSessionsByTag(tag)
                    .OrderBy(session => session.Starts)
                    .ThenBy(session => session.Title)
                    .ToList();
        }
    }
}
