using DuoAuthenticator.Services;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Venz.Services;
using Windows.Security.Cryptography;

namespace DuoAuthenticator.Model
{
    public class Instance
    {
        private IInstanceActivationService InstanceActivationService;
        private OneTimePasswordGenerator OneTimePassword = new OneTimePasswordGenerator();

        public Instance(IInstanceActivationService instanceActivationService)
        {
            InstanceActivationService = instanceActivationService;
        }

        public String GetPasscode()
        {
            var passcode = OneTimePassword.GetNext(App.Settings.OneTimePasswordSecret, App.Settings.OneTimePasswordCounter);
            App.Settings.OneTimePasswordCounter++;
            return passcode;
        }

        public async Task<OperationResult> ActivateAsync(String activationCode)
        {
            if (String.IsNullOrWhiteSpace(activationCode))
                return OperationResult.CreateFailed("Activation code is empty.");

            var activationCodeParts = activationCode.Split('-', StringSplitOptions.RemoveEmptyEntries);
            if (activationCodeParts.Length != 2)
                return OperationResult.CreateFailed("Activation code is invalid.");

            var host = Encoding.UTF8.GetString(CryptographicBuffer.DecodeFromBase64String(activationCodeParts[1]).ToArray());
            var code = activationCodeParts[0];
            var activationResult = await InstanceActivationService.ActivateAsync(host, code).ConfigureAwait(false);
            if (activationResult.IsSuccessful)
            {
                App.Settings.OneTimePasswordCounter = 1;
                App.Settings.OneTimePasswordSecret = activationResult.Value.OneTimePasswordSecret;
            }
            return activationResult;
        }
    }
}
