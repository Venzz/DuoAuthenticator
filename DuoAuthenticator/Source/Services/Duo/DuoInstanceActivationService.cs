using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;

namespace DuoAuthenticator.Services.Duo
{
    public class DuoInstanceActivationService: IInstanceActivationService
    {
        public async Task<OperationResult<IInstanceSettings>> ActivateAsync(String url, String code)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(new Uri($"https://{url}/push/v2/activation/{code}"), new HttpFormUrlEncodedContent(new Dictionary<String, String>()));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonObject.Parse(responseString);
            if (!responseObject.ContainsKey("stat") || responseObject.GetNamedString("stat") != "OK" || !responseObject.ContainsKey("response"))
                return OperationResult.CreateFailed<IInstanceSettings>("Instance activation failed.");

            return OperationResult.CreateSuccessful<IInstanceSettings>(new DuoInstanceSettings(responseObject.GetNamedObject("response")));
        }
    }
}
