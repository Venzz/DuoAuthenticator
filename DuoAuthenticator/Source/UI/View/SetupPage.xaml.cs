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

        private async void OnActivateViaEmailMessageTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            var url = EmailMessageUrl.Text;
            EmailMessageUrl.Text = "";

            await Context.ActivateViaEmailMessageAsync(url);
            Navigation.Navigate(typeof(PasscodePage));
        }

        private async void OnActivateViaExportedSettingsTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            await Context.ActivateViaExportedSettingsAsync(ExportedContent.Text);
            Navigation.Navigate(typeof(PasscodePage));
        }
    }
}
