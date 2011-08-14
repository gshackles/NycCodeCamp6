using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CodeCamp.Core.Entities;

namespace CodeCamp.Core.DataAccess
{
    public class CodeCampRepository
    {
        private IList<Speaker> _speakers;
        private IList<Session> _sessions;
		private IList<Sponsor> _sponsors;
        private IDictionary<string, List<Session>> _tags;
		
		public int CurrentVersion { get; private set; }
		
        public CodeCampRepository(string xml)
        {
			if (string.IsNullOrEmpty(xml))
			{
				_speakers = new List<Speaker>();
				_sessions = new List<Session>();
				_tags = new Dictionary<string, List<Session>>();
				_sponsors = new List<Sponsor>();
			}
			else
			{
				loadXml(xml);
			}
        }

        private void loadXml(string xml)
        {
            var doc = XElement.Parse(xml);
			
			CurrentVersion = int.Parse(doc.Attribute("version").Value);

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
						Tags = session.Element("Tags").Elements("Tag").Select(tag => tag.Value).ToList()
                    }
                ).ToList();

            _tags =
                (
                    from session in _sessions
					from sessionTag in session.Tags
                    group session by sessionTag into tag
                    select new
                    {
                        Name = tag.Key,
                        Sessions = tag.ToList()
                    }
                ).ToDictionary(track => track.Name, track => track.Sessions);
			
			_sponsors =
				(
					from sponsor in doc.Element("Sponsors").Descendants("Sponsor")
					select new Sponsor
					{
						Name = sponsor.Element("Name").Value,
						Website = sponsor.Element("Website").Value
					}
				).ToList();
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

        public IList<Session> GetSessionsByTag(string tag)
        {
            if (!_tags.ContainsKey(tag))
                return null;

            return _tags[tag];
        }

        public IList<string> GetTags()
        {
            return _tags.Keys.ToList();
        }
		
		public IList<Sponsor> GetSponsors()
		{
			return _sponsors;
		}
    }
}
