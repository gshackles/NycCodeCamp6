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
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var room = App.CodeCampService.Repository.GetRoom(NavigationContext.QueryString["key"]);

            DataContext = new MapViewModel(room.Name, room.Key);
        }

        private void MapImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            MapCanvas.Height = MapImage.ActualHeight;
            MapCanvas.Width = MapImage.ActualWidth;
        }
    }
}