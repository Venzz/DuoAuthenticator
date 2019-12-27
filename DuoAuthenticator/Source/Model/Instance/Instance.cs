using DuoAuthenticator.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Venz;
using Windows.Web.Http;

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

        public async Task ActivateAsync(String url)
        {
            var userAgent = "";
            if (url.Contains("android"))
                userAgent = "Mozilla/5.0 (Linux; Android 9; Nokia 6.1 Build/PPR1.180610.011; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/74.0.3729.157 Mobile Safari/537.36";
            else if (url.Contains("windows"))
                userAgent = "Mozilla/5.0 (Windows Phone 10.0; Android 6.0.1; Microsoft; Lumia 950 XL Dual SIM) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Mobile Safari/537.36 Edge/15.15063";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            var responseString = await client.GetStringAsync(new Uri(url)).AsTask().ConfigureAwait(false);

            var activationString = responseString.Between("url=", "\"").HtmlUnescape();
            var uri = new Uri(activationString.Replace("duo://", "https://"));
            var host = uri.Host.Replace("m.", "api.").Replace("m-", "api-");
            var code = uri.PathAndQuery.Split(new Char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
            var activationResult = await InstanceActivationService.ActivateAsync(host, code).ConfigureAwait(false);
            if (activationResult.IsSuccessful)
            {
                App.Settings.OneTimePasswordCounter = 1;
                App.Settings.OneTimePasswordSecret = activationResult.Value.OneTimePasswordSecret;
            }
        }
    }
}
