using System;
using System.ComponentModel.Composition.Hosting;
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
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            _container = new CompositionContainer(catalog);
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
            var activator = new MEFJobActivator(_container);
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
        public void UseNinjectActivator_PassesCorrectActivator()
        {
            var configuration = new Mock<IBootstrapperConfiguration>();
            
            configuration.Object.UseMEFActivator(_container);

            configuration.Verify(x => x.UseActivator(It.IsAny<MEFJobActivator>()));
        }
    }
}
