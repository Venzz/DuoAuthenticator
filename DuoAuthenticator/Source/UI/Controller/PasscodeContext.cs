using System;
using System.Threading.Tasks;
using Venz.Data;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Json;
using Windows.Storage;

namespace DuoAuthenticator.UI.Controller
{
    public class PasscodeContext: ObservableObject, IDisposable
    {
        private Boolean IsDisposed;

        public String Passcode { get; private set; }
        public Double RefreshIndicatorProgress { get; private set; }

        public Task InitializeAsync() => Task.Run(() =>
        {
            Start();
        });

        private async void Start() => await Task.Run(async () =>
        {
            while (!IsDisposed)
            {
                Passcode = App.Model.Instance.GetPasscode();
                RefreshIndicatorProgress = 0;
                OnPropertyChanged(nameof(RefreshIndicatorProgress), nameof(Passcode));

                while (RefreshIndicatorProgress < 15)
                {
                    await Task.Delay(1000);
                    RefreshIndicatorProgress = (RefreshIndicatorProgress + 1) % 16;
                    OnPropertyChanged(nameof(RefreshIndicatorProgress));
                }
            }
        });

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

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
