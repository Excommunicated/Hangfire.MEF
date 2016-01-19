using System;
using System.ComponentModel.Composition.Hosting;
using Hangfire;

namespace HangFire.MEF
{
    public static class MEFBootstrapperConfigurationExtensions
    {
        [Obsolete("Please use `GlobalConfiguration.Configuration.UseUnityActivator` method instead. Will be removed in version 2.0.0.")]
        public static void UseMEFActivator(this IBootstrapperConfiguration configuration, CompositionContainer container)
        {
            configuration.UseActivator(new MEFJobActivator(container));
        }
    }
}