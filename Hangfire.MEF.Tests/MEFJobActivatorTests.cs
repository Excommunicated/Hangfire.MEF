using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Linq;
using System.Reflection;
using HangFire.MEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Hangfire.MEF.Tests
{
    [TestClass]
    public class MEFJobActivatorTests
    {
        private CompositionContainer _container;

        [TestInitialize]
        public void Setup()
        {
            RegistrationBuilder builder = new RegistrationBuilder();
            builder.ForType<Disposable>().Export().SetCreationPolicy(CreationPolicy.NonShared);
            builder.ForType<SingletonDisposable>().Export().SetCreationPolicy(CreationPolicy.Shared);
            _container = new CompositionContainer(new AssemblyCatalog(Assembly.GetExecutingAssembly(),builder));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_ThrowsAnException_WhenContainerIsNull()
        {
            var activator = new MEFJobActivator(null);

        }

        [TestMethod]
        public void Class_IsBasedOnJobActivator()
        {
            var activator = CreateActivator();
            Assert.IsInstanceOfType(activator, typeof(JobActivator));
        }

        [TestMethod]
        public void ActivateJob_CallsMEF()
        {
            var activator = new MEFJobActivator(_container);

            var result = activator.ActivateJob(typeof (IMEFTestClass)) as IMEFTestClass;
            Assert.AreEqual(100,result.TestValue);
        }

        [TestMethod]
        public void ActivateJob_CallsMEF_PropertyImports()
        {
            var activator = new MEFJobActivator(_container);

            var result = activator.ActivateJob(typeof(MEFTestClassWrapper)) as MEFTestClassWrapper;
            Assert.AreEqual(100, result.MEFTestClass.TestValue);
        }

        [TestMethod]
        public void UseMEFActivator_PassesCorrectActivator()
        {
            var configuration = new Mock<IBootstrapperConfiguration>();
            
            configuration.Object.UseMEFActivator(_container);

            configuration.Verify(x => x.UseActivator(It.IsAny<MEFJobActivator>()));
        }

        private MEFJobActivator CreateActivator()
        {
            var activator = new MEFJobActivator(_container);
            return activator;
        }

        class Disposable: IDisposable
        {
            public bool Disposed { get; set; }
            public void Dispose()
            {
                Disposed = true;
            }
        }
       
        class SingletonDisposable : IDisposable
        {
            public bool Disposed { get; set; }
            public void Dispose()
            {
                Disposed = true;
            }
        }
    }
}
