using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using NycCodeCamp.WP7App.ViewModels;

namespace NycCodeCamp.WP7App.Pages
{
    public partial class Map : PhoneApplicationPage
    {
        public Map()
        {
            InitializeComponent();

            Loaded += Map_Loaded;
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MapViewModel(NavigationContext.QueryString["name"],
                                           NavigationContext.QueryString["key"]);
        }
    }
}