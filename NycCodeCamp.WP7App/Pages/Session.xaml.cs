﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Entities = CodeCamp.Core.Entities;

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

        private void SpeakerSelected(object sender, MouseButtonEventArgs e)
        {
            string email = ((Entities.Session) ((TextBlock) sender).DataContext).Speaker.Email;

            NavigationService.Navigate(new Uri("/Pages/Speaker.xaml?email=" + HttpUtility.UrlEncode(email),
                                               UriKind.Relative));
        }
    }
}