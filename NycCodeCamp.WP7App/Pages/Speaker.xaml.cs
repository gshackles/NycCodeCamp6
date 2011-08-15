using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Entities = CodeCamp.Core.Entities;

namespace NycCodeCamp.WP7App.Pages
{
    public partial class Speaker : PhoneApplicationPage
    {
        private Entities.Speaker _speaker;

        public Speaker()
        {
            InitializeComponent();

            Loaded += Speaker_Loaded;
        }

        void Speaker_Loaded(object sender, RoutedEventArgs e)
        {
            _speaker = App.CodeCampService.Repository.GetSpeaker(NavigationContext.QueryString["key"]);

            DataContext = _speaker;
        }

        private void EmailSpeaker(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_speaker.Email))
            {
                var emailTask = new EmailComposeTask();
                emailTask.To = _speaker.Email;
                emailTask.Show();
            }
        }

        private void GoToWebsite(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_speaker.Website))
            {
                var browserTask = new WebBrowserTask();
                browserTask.URL = _speaker.Website;
                browserTask.Show();
            }
        }
    }
}