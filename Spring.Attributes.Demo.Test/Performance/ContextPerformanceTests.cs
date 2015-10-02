using System;

using Com.QueoFlow.Spring.Attributes.Demo.Persistence;
using Com.QueoFlow.Spring.Attributes.Demo.Persistence.Impl;
using Com.QueoFlow.Spring.Attributes.Demo.Test.Persistence;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test.Performance {
    [TestClass]
    public class ContextPerformanceTests: PersistenceBaseTest {

        [TestMethod]
        public void TestGetObject() {
            
            object[] arguments = { };
            for (int i = 0; i < 100000; i++) {
                IDataRepository repo = Context.GetObject(typeof(DataRepository).FullName, arguments) as IDataRepository;
                Assert.IsNotNull(repo);
            }
        }
    }
}
