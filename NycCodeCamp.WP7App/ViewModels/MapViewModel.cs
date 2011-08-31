﻿using System;

namespace NycCodeCamp.WP7App.ViewModels
{
    public class MapViewModel
    {
        public string Name { get; set; }

        public string FilePath { get; set; }

        public MapViewModel(string name, string filename)
        {
            Name = name;
            FilePath = "../Images/" + filename;
        }
    }
}
