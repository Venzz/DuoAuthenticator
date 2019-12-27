using DuoAuthenticator.ViewModel;
using System;
using Windows.UI.Xaml.Controls;

namespace DuoAuthenticator.View
{
    public sealed partial class SetupPage: Page
    {
        private SetupContext Context = new SetupContext();

        public SetupPage()
        {
            InitializeComponent();
            DataContext = Context;
        }

        private async void OnActivateViaEmailMessageTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            var url = EmailMessageUrl.Text;
            EmailMessageUrl.Text = "";

            await Context.ActivateViaEmailMessageAsync(url);
            Frame.Navigate(typeof(PasscodePage));
        }

        private async void OnActivateViaExportedSettingsTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            await Context.ActivateViaExportedSettingsAsync(ExportedContent.Text);
            Frame.Navigate(typeof(PasscodePage));
        }
    }
}
