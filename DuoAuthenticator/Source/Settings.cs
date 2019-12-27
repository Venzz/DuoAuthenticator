using System;
using Venz.UI.Xaml;

namespace DuoAuthenticator
{
    public class Settings: ApplicationSettings
    {
        public String OneTimePasswordSecret
        {
            get => Get(nameof(OneTimePasswordSecret), "");
            set => Set(nameof(OneTimePasswordSecret), value);
        }

        public UInt64 OneTimePasswordCounter
        {
            get => Get(nameof(OneTimePasswordCounter), 0UL);
            set => Set(nameof(OneTimePasswordCounter), value);
        }
    }
}
