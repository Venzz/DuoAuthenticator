using System;
using System.Threading.Tasks;
using Venz.Services;

namespace DuoAuthenticator.Services
{
    public interface IInstanceActivationService
    {
        Task<OperationResult<IInstanceSettings>> ActivateAsync(String url, String code);
    }
}
