using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Entities = CodeCamp.Core.Entities;

namespace NycCodeCamp.WP7App.Pages
{
    public partial class Sponsor : PhoneApplicationPage
    {
        private readonly Entities.Sponsor _sponsor;

        public Sponsor()
        {
            InitializeComponent();

            _sponsor = PhoneApplicationService.Current.State["SelectedSponsor"] as Entities.Sponsor;
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