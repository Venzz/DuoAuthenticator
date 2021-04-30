﻿using System;
using System.Threading.Tasks;
using Venz.Data;
using Venz.Services;
using Windows.Data.Json;

namespace DuoAuthenticator.UI.Controller
{
    public class SetupContext: ObservableObject
    {
        public String Passcode { get; private set; }
        public Double RefreshIndicatorProgress { get; private set; }

        public Task<OperationResult> ActivateViaActivationCodeAsync(String activationCode) => App.Model.Instance.ActivateAsync(activationCode);

        public async Task ActivateViaExportedSettingsAsync(String value) => await Task.Run(() =>
        {
            var importedContent = JsonObject.Parse(value);
            App.Settings.OneTimePasswordSecret = importedContent.GetNamedString("secret");
            App.Settings.OneTimePasswordCounter = (UInt64)importedContent.GetNamedNumber("counter");
        });
    }
}
