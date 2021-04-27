using System;
using System.Threading.Tasks;
using Venz.Data;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Json;
using Windows.Storage;

namespace DuoAuthenticator.UI.Controller
{
    public class PasscodeContext: ObservableObject
    {
        public String Passcode { get; private set; }

        public Task InitializeAsync()
        {
            Passcode = App.Model.Instance.GetPasscode();
            OnPropertyChanged(nameof(Passcode));
            return Task.CompletedTask;
        }

        public void GenerateNextPasscode()
        {
            Passcode = App.Model.Instance.GetPasscode();
            OnPropertyChanged(nameof(Passcode));
        }

        public async Task ExportAsync() => await Task.Run(async () =>
        {
            var exportedSettingsFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("exported_settings.json", CreationCollisionOption.ReplaceExisting).AsTask().ConfigureAwait(false);
            var content = new JsonObject();
            content["secret"] = JsonValue.CreateStringValue(App.Settings.OneTimePasswordSecret);
            content["counter"] = JsonValue.CreateNumberValue(App.Settings.OneTimePasswordCounter);
            await FileIO.WriteTextAsync(exportedSettingsFile, content.Stringify()).AsTask().ConfigureAwait(false);

            var dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(content.Stringify());

            await App.Dispatcher.RunAsync(() => {
                Clipboard.SetContent(dataPackage);
                Clipboard.Flush();
            });

            App.Settings.OneTimePasswordSecret = "";
            App.Settings.OneTimePasswordCounter = 0;
        });
    }
}
