using System;
using Windows.Data.Json;

namespace DuoAuthenticator.Services.Duo
{
    public class DuoInstanceSettings: IInstanceSettings
    {
        public String OneTimePasswordSecret { get; }

        public DuoInstanceSettings(JsonObject value)
        {
            if (value.ContainsKey("hotp_secret"))
                OneTimePasswordSecret = value.GetNamedString("hotp_secret");
        }
    }
}
