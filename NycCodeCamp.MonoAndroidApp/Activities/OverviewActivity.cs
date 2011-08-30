using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using CodeCamp.Core.DataAccess;
using CodeCamp.Core.Messaging;
using CodeCamp.Core.Messaging.Messages;
using CodeCamp.Core.ViewModels;
using NycCodeCamp.MonoAndroidApp.ListAdapters;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "NYC Code Camp 6", MainLauncher = true, Icon = "@drawable/icon")]
    public class OverviewActivity : CampActivityBase
    {
        private ListView _slots;
        private ProgressDialog _waitingDialog = null;
        private OverviewSlotAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Overview);

            _slots = FindViewById<ListView>(Resource.Id.Slots);

            subscribeToMessages();

            CodeCampApplication.CodeCampService = new CodeCampService("http://codecamps.gregshackles.com/v1", "sample");

            reloadData();

            _slots.ItemClick += _slots_ItemClick;
        }

        void _slots_ItemClick(object sender, ItemEventArgs e)
        {
            var session = _adapter.GetItemAtPosition(e.Position);

            var intent = new Intent();
            intent.SetClass(this, typeof (SessionActivity));
            intent.PutExtra("key", session.Key);

            StartActivity(intent);
        }

        private void reloadData()
        {
            var allSessions = CodeCampApplication.CodeCampService.Repository.GetSessions();
            var viewModel = new CampOverviewViewModel(allSessions);
            
            _adapter = new OverviewSlotAdapter(this, viewModel.UpcomingSlots);

            _slots.Adapter = _adapter;
        }

        private void showWaitingDialog(string message)
        {
            _waitingDialog = ProgressDialog.Show(this, null, message);
        }

        private void hideWaitingDialog()
        {
            if (_waitingDialog != null && _waitingDialog.IsShowing)
            {
                _waitingDialog.Dismiss();
            }
        }

        private void subscribeToMessages()
        {
            MessageHub.Instance.Subscribe<StartedCheckingForUpdatedScheduleMessage>(
                msg => RunOnUiThread(() => showWaitingDialog("Checking for updated schedule")));

            MessageHub.Instance.Subscribe<ErrorCheckingForUpdatedScheduleMessage>(msg => RunOnUiThread(() =>
            {
                hideWaitingDialog();

                Toast.MakeText(this,
                                "Unable to check for an updated schedule, please try again later",
                                ToastLength.Short).Show();
            }));

            MessageHub.Instance.Subscribe<NoUpdatedScheduleAvailableMessage>(msg => RunOnUiThread(() =>
            {
                hideWaitingDialog();

                Toast.MakeText(this, "Your schedule is already up to date", ToastLength.Short).Show();
            }));

            MessageHub.Instance.Subscribe<FoundUpdatedScheduleMessage>(msg => RunOnUiThread(() =>
                hideWaitingDialog()));

            MessageHub.Instance.Subscribe<StartedDownloadingUpdatedScheduleMessage>(
                msg => RunOnUiThread(() => showWaitingDialog("Downloading updated event details")));

            MessageHub.Instance.Subscribe<ErrorDownloadingUpdatedScheduleMessage>(msg => RunOnUiThread(() =>
            {
                hideWaitingDialog();

                Toast.MakeText(this, "Unable to download the event details, please try again later", ToastLength.Short).Show();
            }));

            MessageHub.Instance.Subscribe<FinishedUpdatingScheduleMessage>(msg => RunOnUiThread(() => 
            {
                reloadData();

                hideWaitingDialog();
            }));
        }
    }
}

