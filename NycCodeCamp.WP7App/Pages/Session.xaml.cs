using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;

namespace NycCodeCamp.WP7App.Pages
{
    public partial class Session : PhoneApplicationPage
    {
        public Session()
        {
            InitializeComponent();

            Loaded += Session_Loaded;
        }

        void Session_Loaded(object sender, RoutedEventArgs e)
        {
            var session = App.CodeCampService.Repository.GetSession(NavigationContext.QueryString["key"]);
            DataContext = session;
        }
    }
}