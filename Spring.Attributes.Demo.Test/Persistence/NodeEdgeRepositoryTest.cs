using Com.QueoFlow.Spring.Attributes.Demo.Persistence;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test.Persistence {
    [TestClass]
    public class NodeEdgeRepositoryTest:PersistenceBaseTest {

        [Resource]
        private IDataRepository _dataRepository;

        /// <summary>
        /// Testst ob <see cref="IDataRepository"/> korrekt über das Resource Attribut injected wird
        /// </summary>
        [TestMethod]
        public void TestGetsInjected() {
            Assert.IsNotNull(_dataRepository);
        }
        
    }
}