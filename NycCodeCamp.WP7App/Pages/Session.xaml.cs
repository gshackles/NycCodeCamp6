using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using CoreEntities = CodeCamp.Core.Entities;

namespace NycCodeCamp.WP7App.Pages
{
    public partial class Session : PhoneApplicationPage
    {
        private CodeCamp.Core.Entities.Session _session;

        public Session()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _session = App.CodeCampService.Repository.GetSession(NavigationContext.QueryString["key"]);
            DataContext = _session;
        }

        private void SpeakerSelected(object sender, MouseButtonEventArgs e)
        {
            string email = ((CoreEntities.Session)((TextBlock)sender).DataContext).Speaker.Email;

            NavigationService.Navigate(new Uri("/Pages/Speaker.xaml?email=" + HttpUtility.UrlEncode(email),
                                               UriKind.Relative));
        }

        private void RoomSelected(object sender, MouseButtonEventArgs e)
        {
                NavigationService.Navigate(
                    new Uri("/Pages/Map.xaml?key=" + HttpUtility.UrlEncode(_session.RoomKey), UriKind.Relative));
        }
    }
}