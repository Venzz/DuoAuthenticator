using DuoAuthenticator.ViewModel;
using System;
using Venz.UI.Xaml;
using Windows.ApplicationModel.DataTransfer;
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

        protected override async void SetState(FrameNavigation.Parameter navigationParameter, FrameNavigation.Parameter stateParameter)
        {
            await Context.InitializeAsync();
        }

        private async void OnExportTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            await Context.ExportAsync();
            Navigation.Navigate(typeof(SetupPage));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs args)
        {
            base.OnNavigatedFrom(args);
            Context.Dispose();
        }
    }
}
