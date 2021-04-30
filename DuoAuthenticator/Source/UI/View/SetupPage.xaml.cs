using DuoAuthenticator.UI.Controller;
using System;
using Venz.UI.Xaml;

namespace DuoAuthenticator.UI.View
{
    public sealed partial class SetupPage: Page
    {
        private SetupContext Context = new SetupContext();

        public SetupPage()
        {
            InitializeComponent();
            DataContext = Context;
        }

        protected override void SetState(FrameNavigation.Parameter navigationParameter, FrameNavigation.Parameter stateParameter)
        {
            Navigation.ResetBackStack();
        }

        private async void OnActivateViaCodeTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            var activationCode = ActivationCode.Text;
            var activationResult = await Context.ActivateViaActivationCodeAsync(activationCode);
            if (!activationResult.IsSuccessful)
            {
                await Venz.Windows.MessageDialog.ShowAsync("Activation Failed", activationResult.Message);
                return;
            }

            ActivationCode.Text = "";
            Navigation.Navigate(typeof(PasscodePage));
        }

        private async void OnActivateViaExportedSettingsTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            await Context.ActivateViaExportedSettingsAsync(ExportedContent.Text);
            Navigation.Navigate(typeof(PasscodePage));
        }
    }
}
