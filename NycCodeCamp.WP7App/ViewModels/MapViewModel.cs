using System;

namespace NycCodeCamp.WP7App.ViewModels
{
    public class MapViewModel
    {
        public string Name { get; set; }

        public string FilePath { get; set; }

        public MapViewModel(string name, string key)
        {
            Name = name;
            FilePath = "../Images/Maps/" + key + ".jpg";
        }
    }
}
