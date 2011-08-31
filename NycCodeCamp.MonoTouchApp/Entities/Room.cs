using System;

namespace NycCodeCamp.MonoTouchApp
{
	public class Room
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public string Filename { get; private set; }
		
		public Room (string name, string description, string filename)
		{
			Name = name;
			Description = description;
			Filename = filename;
		}
	}
}
