using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Venz.Services;
using Windows.Data.Json;
using Windows.Web.Http;

namespace DuoAuthenticator.Services
{
    public class DuoInstanceActivationService: IInstanceActivationService
    {
        public async Task<OperationResult<IInstanceSettings>> ActivateAsync(String host, String code)
        {
            var client = new HttpClient();
            var uri = new Uri($"https://{host}/push/v2/activation/{code}?customer_protocol=1");
            var response = await client.PostAsync(uri, new HttpFormUrlEncodedContent(new Dictionary<String, String>()));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonObject.Parse(responseString);
            if (!responseObject.ContainsKey("stat") || responseObject.GetNamedString("stat") != "OK" || !responseObject.ContainsKey("response"))
                return OperationResult.CreateFailed<IInstanceSettings>("Failed to activate the code.");

            return OperationResult.CreateSuccessful<IInstanceSettings>(new DuoInstanceSettings(responseObject.GetNamedObject("response")));
        }
    }
}
