using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using CodeCamp.Core.Messaging;
using CodeCamp.Core.Messaging.Messages;
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using NycCodeCamp.WP7App.ViewModels;
using Entities = CodeCamp.Core.Entities;

namespace NycCodeCamp.WP7App
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ProgressOverlay _progress;

        public MainPage()
        {
            InitializeComponent();

            subscribeToMessages();

            DataContext = new MainViewModel();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void SessionSelected(object sender, RoutedEventArgs e)
        {
            var selectedSession = ((FrameworkElement)sender).DataContext as Entities.Session;

            NavigationService.Navigate(new Uri("/Pages/Session.xaml?key=" + selectedSession.Key, UriKind.Relative));
        }

        private void showWaitingDialog(string message)
        {
            _progress = new ProgressOverlay();
            _progress.Style = (Style)App.Current.Resources["ProgressBarStyle"];
            
            ApplicationBar.IsVisible = false;
            _progress.Content = message;
            LayoutRoot.Children.Add(_progress);
        }

        private void hideWaitingDialog()
        {
            LayoutRoot.Children.Remove(_progress);
            _progress = null;

            ApplicationBar.IsVisible = true;
        }

        private void subscribeToMessages()
        {
            MessageHub.Instance.Subscribe<StartedCheckingForUpdatedScheduleMessage>(
                msg => showWaitingDialog("Checking for updated schedule"));

            MessageHub.Instance.Subscribe<ErrorCheckingForUpdatedScheduleMessage>(msg =>
            {
                hideWaitingDialog();

                MessageBox.Show("Unable to check for an updated schedule, please try again later", "Error",
                                MessageBoxButton.OK);
            });

            MessageHub.Instance.Subscribe<NoUpdatedScheduleAvailableMessage>(msg =>
            {
                hideWaitingDialog();

                MessageBox.Show("Your schedule is already up to date", "", MessageBoxButton.OK);
            });

            MessageHub.Instance.Subscribe<FoundUpdatedScheduleMessage>(msg => hideWaitingDialog());

            MessageHub.Instance.Subscribe<StartedDownloadingUpdatedScheduleMessage>(
                msg => showWaitingDialog("Downloading updated event details"));

            MessageHub.Instance.Subscribe<ErrorDownloadingUpdatedScheduleMessage>(msg =>
            {
                hideWaitingDialog();

                MessageBox.Show("Unable to download the event details, please try again later", "Error",
                                MessageBoxButton.OK);
            });

            MessageHub.Instance.Subscribe<FinishedUpdatingScheduleMessage>(msg =>
            {
                DataContext = new MainViewModel();

                hideWaitingDialog();
            });
        }

        private void RefreshSchedule(object sender, EventArgs e)
        {
            App.CodeCampService.CheckForUpdatedSchedule();
        }

        private void Pivot_LoadingPivotItem(object sender, PivotItemEventArgs e)
        {
            ApplicationBar.IsVisible = (e.Item == OverviewPivotItem);
        }

        private void TagSelected(object sender, RoutedEventArgs e)
        {
            string tag = ((FrameworkElement) sender).DataContext as string;

            MessageBox.Show(tag, "Tag selected", MessageBoxButton.OK);
        }

        private void SpeakerSelected(object sender, RoutedEventArgs e)
        {
            var selectedSpeaker = ((FrameworkElement) sender).DataContext as Entities.Speaker;

            NavigationService.Navigate(new Uri("/Pages/Speaker.xaml?key=" + selectedSpeaker.Key, UriKind.Relative));
        }

        private void SponsorSelected(object sender, RoutedEventArgs e)
        {
            var selectedSponsor = ((FrameworkElement) sender).DataContext as Entities.Sponsor;

            var browserTask = new WebBrowserTask();
            browserTask.URL = selectedSponsor.Website;
            browserTask.Show();
        }
    }
}