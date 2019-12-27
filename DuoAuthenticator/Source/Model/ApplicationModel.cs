using DuoAuthenticator.Services.Duo;

namespace DuoAuthenticator.Model
{
    public class ApplicationModel
    {
        public Instance Instance { get; }

        public ApplicationModel()
        {
            Instance = new Instance(new DuoInstanceActivationService());
        }
    }
}
