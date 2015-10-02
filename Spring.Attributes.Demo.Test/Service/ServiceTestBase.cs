using Com.QueoFlow.Spring.Attributes.Demo.Test.Persistence;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test.Service {
    
    /// <summary>
    /// Basisklasse für alle ServiceTest-Klassen
    /// </summary>
    [TestClass]
    public class ServiceTestBase : PersistenceBaseTest {
        
        protected override ApplicationContextResources GetApplicationContextResources() {
            ApplicationContextResources applicationContextRessources = base.GetApplicationContextResources();
            return applicationContextRessources;
        }
    }
}