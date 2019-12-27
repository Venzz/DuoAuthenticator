using System;

namespace DuoAuthenticator.Services
{
    public interface IInstanceSettings
    {
        String OneTimePasswordSecret { get; }
    }
}
