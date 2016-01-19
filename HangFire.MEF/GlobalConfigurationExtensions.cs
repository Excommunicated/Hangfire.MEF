using System;
using System.ComponentModel.Composition.Hosting;
using Hangfire;
using Hangfire.Annotations;

namespace HangFire.MEF
{
    public static  class GlobalConfigurationExtensions
    {
        public static IGlobalConfiguration<MEFJobActivator> UseMEFActivator(
           [NotNull] this IGlobalConfiguration configuration, CompositionContainer container)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (container == null) throw new ArgumentNullException("container");

            return configuration.UseActivator(new MEFJobActivator(container));
        }
    }
}