using System.ComponentModel.Composition;

namespace Hangfire.MEF.Tests
{
    [Export]
    public class MEFTestClassWrapper
    {
        [Import]
        public IMEFTestClass MEFTestClass { get; set; }
    }
}