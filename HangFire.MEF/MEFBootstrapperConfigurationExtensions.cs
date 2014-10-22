using System.ComponentModel.Composition.Hosting;
using Hangfire;

namespace HangFire.MEF
{
    public static class MEFBootstrapperConfigurationExtensions
    {
        public static void UseMEFActivator(this IBootstrapperConfiguration configuration, CompositionContainer container)
        {
            configuration.UseActivator(new MEFJobActivator(container));
        }
    }
}