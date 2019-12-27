using DuoAuthenticator.Model;
using DuoAuthenticator.View;
using System;
using System.Threading.Tasks;
using Venz.UI.Xaml;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

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

        protected override Task OnManuallyActivatedAsync(Frame frame, Boolean newInstance, String args)
        {
            if (frame.Content == null)
            {
                ApplicationView.PreferredLaunchViewSize = new Size(360, 500);
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(360, 500));
                ApplicationView.GetForCurrentView().TryResizeView(new Size(360, 500));
                frame.Navigate(String.IsNullOrWhiteSpace(App.Settings.OneTimePasswordSecret) ? typeof(SetupPage) : typeof(PasscodePage));
            }
            return Task.CompletedTask;
        }
    }
}
