using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using CoreEntities = CodeCamp.Core.Entities;

namespace NycCodeCamp.WP7App.Pages
{
    public partial class Sponsor : PhoneApplicationPage
    {
        private CoreEntities.Sponsor _sponsor;

        public Sponsor()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _sponsor = PhoneApplicationService.Current.State["SelectedSponsor"] as CoreEntities.Sponsor;
            DataContext = _sponsor;
        }

        private void GoToWebsite(object sender, EventArgs e)
        {
            var browserTask = new WebBrowserTask();
            browserTask.URL = _sponsor.Website;
            browserTask.Show();   
        }
    }
}