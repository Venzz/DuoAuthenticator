using DuoAuthenticator.ViewModel;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DuoAuthenticator.View
{
    public sealed partial class PasscodePage: Page
    {
        private PasscodeContext Context = new PasscodeContext();

        public PasscodePage()
        {
            InitializeComponent();
            DataContext = Context;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs args)
        {
            await Context.InitializeAsync();
        }

        private async void OnExportTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            await Context.ExportAsync();
            Frame.Navigate(typeof(SetupPage));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs args)
        {
            base.OnNavigatingFrom(args);
            Context.Dispose();
        }
    }
}
