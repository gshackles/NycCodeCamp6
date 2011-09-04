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
		private IList<string> _sponsorTiers;
		private IList<Room> _rooms;
		
		public int CurrentVersion { get; private set; }
		
        public CodeCampRepository(string xml)
        {
			_speakers = new List<Speaker>();
			_sessions = new List<Session>();
			_tags = new Dictionary<string, List<Session>>();
			_sponsors = new List<Sponsor>();
			_sponsorTiers = new List<string>();
			_rooms = new List<Room>();
			
			if (!string.IsNullOrEmpty(xml))
			{
				loadXml(xml);
			}
        }

        private void loadXml(string xml)
        {
            var doc = XElement.Parse(xml);
			
			CurrentVersion = int.Parse(doc.Attribute("Version").Value);
			
			if (doc.Element("Speakers") != null)
			{
	            _speakers =
	                (
	                    from speaker in doc.Element("Speakers").Descendants("Speaker")
	                    select new Speaker
	                    {
	                        Name = speaker.Element("Name").Value,
	                        Bio = insertNewLines(speaker.Element("Bio").Value),
	                        Website = speaker.Element("Website").Value,
	                        Email = speaker.Element("Email").Value
	                    }
	                ).ToList();
			}
			
			if (doc.Element("Sessions") != null)
			{
	            _sessions =
	                (
	                    from session in doc.Element("Sessions").Descendants("Session")
	                    join speaker in _speakers
	                        on session.Element("Speaker").Value equals speaker.Email
	                    select new Session
	                    {
	                        Key = session.Element("Key").Value,
	                        Title = session.Element("Title").Value,
	                        Abstract = insertNewLines(session.Element("Abstract").Value),
							Room = session.Element("Room").Value,
                            RoomKey = session.Element("RoomKey").Value,
	                        Speaker = speaker,
	                        Starts = DateTime.Parse(session.Element("StartDate").Value),
	                        Ends = DateTime.Parse(session.Element("EndDate").Value),
							Tags = session.Element("Tags").Elements("Tag").Select(tag => tag.Value).ToList()
	                    }
	                ).ToList();
			}
			
			if (_sessions.Count > 0)
			{
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
			}
			
			if (doc.Element("Sponsors") != null)
			{
				_sponsors =
					(
						from sponsor in doc.Element("Sponsors").Descendants("Sponsor")
						select new Sponsor
						{
							Name = sponsor.Element("Name").Value,
							Website = sponsor.Element("Website").Value,
							Description = insertNewLines(sponsor.Element("Description").Value),
							Tier = sponsor.Element("Tier").Value
						}
					).ToList();
			}
			
			if (doc.Element("SponsorTiers") != null)
			{
				_sponsorTiers =
					(
						from tier in doc.Element("SponsorTiers").Descendants("Tier")
						select tier.Value
					).ToList();
			}
			
			if (doc.Element("Rooms") != null)
			{
				_rooms =
					(
						from room in doc.Element("Rooms").Descendants("Room")
						select new Room
						{
							Name = room.Element("Name").Value,
							Key = room.Element("Key").Value,
							Description = room.Element("Description").Value
						}
					).ToList();
			}
        }
		
        public Speaker GetSpeaker(string email)
        {
            return _speakers.FirstOrDefault(speaker => speaker.Email == email);
        }

        public IList<Speaker> GetSpeakers()
        {
            return _speakers.Where(speaker => !string.IsNullOrEmpty(speaker.Name)).ToList();
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
            return _tags.Keys.Where(tag => !string.IsNullOrEmpty(tag)).ToList();
        }
		
		public IList<Sponsor> GetSponsors()
		{
			return _sponsors;
		}
		
		public IList<string> GetSponsorTiers()
		{
			return _sponsorTiers;
		}
		
		public IList<Room> GetRooms()
		{
			return _rooms;
		}

        public Room GetRoom(string roomKey)
        {
            return _rooms.FirstOrDefault(room => room.Key == roomKey);
        }
		
		private string insertNewLines(string input)
		{
			return input.Replace("{nl}", Environment.NewLine);
		}
    }
}
