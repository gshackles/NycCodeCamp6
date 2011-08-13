using System;
using System.Collections.Generic;
using System.Linq;
using CodeCamp.Core.Entities;

namespace CodeCamp.Core.DataAccess
{
    public interface ICodeCampRepository
    {
        Speaker GetSpeaker(string speakerKey);
        IList<Speaker> GetSpeakers();

        Session GetSession(string sessionKey);
        IList<Session> GetSessions();
        IList<Session> GetSessionsByTag(string tag);

        IList<string> GetTags();
    }
}
