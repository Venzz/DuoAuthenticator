using DuoAuthenticator.Model;
using DuoAuthenticator.UI.View;
using System;
using System.Threading.Tasks;
using Venz.UI.Xaml;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace DuoAuthenticator
{
    public sealed partial class App: Application
    {
        public static Settings Settings { get; } = new Settings();
        public static ApplicationModel Model { get; } = new ApplicationModel();

        public App()
        {
            InitializeComponent();
        }

        protected override Task OnManuallyActivatedAsync(Frame frame, Boolean newInstance, PrelaunchStage prelaunchStage, String args)
        {
            if (frame.Content == null)
            {
                ApplicationView.PreferredLaunchViewSize = new Size(360, 500);
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(360, 500));
                ApplicationView.GetForCurrentView().TryResizeView(new Size(360, 500));
                var destinationPage = String.IsNullOrWhiteSpace(App.Settings.OneTimePasswordSecret) ? typeof(SetupPage) : typeof(PasscodePage);
                frame.Navigation.Navigate(destinationPage);
            }
            return Task.CompletedTask;
        }

        protected override Task OnUriActivatedAsync(Frame frame, Boolean newInstance, PrelaunchStage prelaunchStage, ProtocolActivatedEventArgs args)
        {
            if (frame.Content == null)
            {
                ApplicationView.PreferredLaunchViewSize = new Size(360, 500);
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(360, 500));
                ApplicationView.GetForCurrentView().TryResizeView(new Size(360, 500));
                var destinationPage = String.IsNullOrWhiteSpace(App.Settings.OneTimePasswordSecret) ? typeof(SetupPage) : typeof(PasscodePage);
                frame.Navigation.Navigate(destinationPage, new FrameNavigation.Parameter("uri", args.Uri.OriginalString));
            }
            return Task.CompletedTask;
        }
    }
}
