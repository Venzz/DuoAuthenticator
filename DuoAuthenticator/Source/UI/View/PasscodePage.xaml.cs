using DuoAuthenticator.UI.Controller;
using System;
using Venz.UI.Xaml;
using Venz.Windows;
using Windows.ApplicationModel.DataTransfer;

namespace DuoAuthenticator.UI.View
{
    public sealed partial class PasscodePage: Page
    {
        private PasscodeContext Context = new PasscodeContext();

        public PasscodePage()
        {
            InitializeComponent();
            DataContext = Context;
        }

        protected override async void SetState(FrameNavigation.Parameter navigationParameter, FrameNavigation.Parameter stateParameter)
        {
            Navigation.ResetBackStack();
            if (navigationParameter.Contains("uri"))
                await MessageDialog.ShowAsync("Activation Failed", "Duo has already been activated.");

            await Context.InitializeAsync();
        }

        private void OnCopyTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            var dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(Context.Passcode);

            Clipboard.SetContent(dataPackage);
            Clipboard.Flush();
        }

        private void OnNextTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            Context.GenerateNextPasscode();
        }

        private async void OnExportTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            await Context.ExportAsync();
            Navigation.Navigate(typeof(SetupPage));
        }
    }
}
