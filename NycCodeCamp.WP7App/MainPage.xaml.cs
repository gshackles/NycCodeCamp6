using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CodeCamp.Core.Messaging;
using CodeCamp.Core.Messaging.Messages;
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
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

        private void TagSelected(object sender, SelectionChangedEventArgs e)
        {
            processSelectedItem<string>(sender, e, tag =>
                NavigationService.Navigate(new Uri("/Pages/SessionsByTag.xaml?tag=" + HttpUtility.UrlEncode(tag), UriKind.Relative)));
        }

        private void SpeakerSelected(object sender, SelectionChangedEventArgs e)
        {
            processSelectedItem<Entities.Speaker>(sender, e, speaker =>
                NavigationService.Navigate(new Uri("/Pages/Speaker.xaml?email=" + HttpUtility.UrlEncode(speaker.Email), UriKind.Relative)));
        }

        private void SponsorSelected(object sender, SelectionChangedEventArgs e)
        {
            processSelectedItem<Entities.Sponsor>(sender, e, sponsor =>
            {
                PhoneApplicationService.Current.State["SelectedSponsor"] = sponsor;
                NavigationService.Navigate(new Uri("/Pages/Sponsor.xaml", UriKind.Relative));
            });
        }

        private void SessionSelected(object sender, SelectionChangedEventArgs e)
        {
            processSelectedItem<Entities.Session>(sender, e, session =>
                NavigationService.Navigate(new Uri("/Pages/Session.xaml?key=" + session.Key, UriKind.Relative)));
        }

        private void processSelectedItem<T>(object sender, SelectionChangedEventArgs e, Action<T> processItem)
        {
            if (e.AddedItems.Count == 0) return;

            T selectedItem = (T) e.AddedItems[0];

            ((ListBox) sender).SelectedItem = null;

            processItem(selectedItem);
        }
    }
}