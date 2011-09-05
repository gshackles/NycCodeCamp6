using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using CoreEntities = CodeCamp.Core.Entities;

namespace NycCodeCamp.WP7App.Pages
{
    public partial class Speaker : PhoneApplicationPage
    {
        private CoreEntities.Speaker _speaker;

        public Speaker()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _speaker = App.CodeCampService.Repository.GetSpeaker(NavigationContext.QueryString["email"]);

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