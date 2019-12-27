using System;
using System.Threading.Tasks;
using Venz.Data;
using Windows.Data.Json;

namespace DuoAuthenticator.ViewModel
{
    public class SetupContext: ObservableObject
    {
        public String Passcode { get; private set; }
        public Double RefreshIndicatorProgress { get; private set; }

        public Task ActivateViaEmailMessageAsync(String url) => App.Model.Instance.ActivateAsync(url);

        public async Task ActivateViaExportedSettingsAsync(String value) => await Task.Run(() =>
        {
            var importedContent = JsonObject.Parse(value);
            App.Settings.OneTimePasswordSecret = importedContent.GetNamedString("secret");
            App.Settings.OneTimePasswordCounter = (UInt64)importedContent.GetNamedNumber("counter");
        });
    }
}
