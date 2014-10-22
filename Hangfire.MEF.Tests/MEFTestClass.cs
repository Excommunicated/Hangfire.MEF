using System.ComponentModel.Composition;

namespace Hangfire.MEF.Tests
{
    [Export(typeof(IMEFTestClass))]
    public class MEFTestClass : IMEFTestClass
    {
        public int TestValue { get { return 100; } }
    }
}