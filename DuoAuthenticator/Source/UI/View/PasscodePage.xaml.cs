﻿using DuoAuthenticator.UI.Controller;
using System;
using Venz.UI.Xaml;
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
            await Context.InitializeAsync();
        }

        private async void OnExportTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            await Context.ExportAsync();
            Navigation.Navigate(typeof(SetupPage));
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
    }
}
