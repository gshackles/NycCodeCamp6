using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CodeCamp.Core.Entities;

namespace CodeCamp.Core.DataAccess
{
    public class XmlCodeCampRepository : ICodeCampRepository
    {
        private IList<Speaker> _speakers;
        private IList<Session> _sessions;
        private IDictionary<string, List<Session>> _tracks;

        public XmlCodeCampRepository(string xml)
        {
            loadXml(xml);
        }

        private void loadXml(string xml)
        {
            var doc = XElement.Parse(xml);

            _speakers =
                (
                    from speaker in doc.Element("Speakers").Descendants("Speaker")
                    select new Speaker
                    {
                        Key = speaker.Element("Key").Value,
                        Name = speaker.Element("Name").Value,
                        Bio = speaker.Element("Bio").Value,
                        Website = speaker.Element("Website").Value,
                        Email = speaker.Element("Email").Value
                    }
                ).ToList();

            _sessions =
                (
                    from session in doc.Element("Sessions").Descendants("Session")
                    join speaker in _speakers
                        on session.Element("Speaker").Value equals speaker.Key
                    select new Session
                    {
                        Key = session.Element("Key").Value,
                        Title = session.Element("Title").Value,
                        Abstract = session.Element("Abstract").Value,
                        Speaker = speaker,
                        Starts = DateTime.Parse(session.Element("StartDate").Value),
                        Ends = DateTime.Parse(session.Element("EndDate").Value),
                        Track = session.Element("Track").Value
                    }
                ).ToList();

            _tracks =
                (
                    from session in _sessions
                    group session by session.Track into track
                    select new
                    {
                        Name = track.Key,
                        Sessions = track.ToList()
                    }
                ).ToDictionary(track => track.Name, track => track.Sessions);
        }

        public Speaker GetSpeaker(string speakerKey)
        {
            return _speakers.FirstOrDefault(speaker => speaker.Key == speakerKey);
        }

        public IList<Speaker> GetSpeakers()
        {
            return _speakers;
        }

        public Session GetSession(string sessionKey)
        {
            return _sessions.FirstOrDefault(session => session.Key == sessionKey);
        }

        public IList<Session> GetSessions()
        {
            return _sessions;
        }

        public IList<Session> GetSessionsByTrack(string track)
        {
            if (!_tracks.ContainsKey(track))
                return null;

            return _tracks[track];
        }

        public IList<string> GetTrackNames()
        {
            return _tracks.Keys.ToList();
        }
    }
}
