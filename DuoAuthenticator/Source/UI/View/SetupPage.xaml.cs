﻿using System;
using Venz.UI.Xaml;
using Venz.Windows;

namespace DuoAuthenticator.UI.View
{
    public sealed partial class SetupPage: Page
    {
        public SetupPage()
        {
            InitializeComponent();
        }

        protected override void SetState(FrameNavigation.Parameter navigationParameter, FrameNavigation.Parameter stateParameter)
        {
            Navigation.ResetBackStack();
            if (navigationParameter.Contains("uri"))
            {
                ActivationCode.Text = ((String)navigationParameter.Get("uri")).Replace("duo://", "").Replace("/", "");
                OnActivateViaCodeTapped(null, null);
            }
        }

        private async void OnActivateViaCodeTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            var activationCode = ActivationCode.Text;
            var activationResult = await App.Model.Instance.ActivateAsync(activationCode);
            if (!activationResult.IsSuccessful)
            {
                await MessageDialog.ShowAsync("Activation Failed", activationResult.Message);
                return;
            }

            ActivationCode.Text = "";
            Navigation.Navigate(typeof(PasscodePage));
        }

        private async void OnActivateViaExportedSettingsTapped(Object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs args)
        {
            var activationResult = await App.Model.Instance.ImportSettingsAsync(ExportedContent.Text);
            if (!activationResult.IsSuccessful)
            {
                await MessageDialog.ShowAsync("Activation Failed", activationResult.Message);
                return;
            }

            ExportedContent.Text = "";
            Navigation.Navigate(typeof(PasscodePage));
        }
    }
}
