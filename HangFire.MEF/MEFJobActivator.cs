using System;
using System.ComponentModel.Composition.Hosting;
using Hangfire;

namespace HangFire.MEF
{
    public class MEFJobActivator : JobActivator
    {
        private readonly CompositionContainer _container;

        public MEFJobActivator(CompositionContainer container)
        {
           if(container == null) throw new ArgumentNullException("container");
           _container = container;
        }

        public override object ActivateJob(Type jobType)
        {
            return _container.GetExportedValueByType(jobType);
        }
    }
}