using System;
using System.Collections.Generic;

using Com.QueoFlow.Spring.Attributes.Demo.Test.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test.Persistence {
    [TestClass]
    public class PersistenceBaseTest : SpringBaseTest {
        [Resource]
        private CreatorUtil _creatorUtil;

        protected CreatorUtil DomainObjectCreatorUtil {
            get { return _creatorUtil; }
        }

        protected override ApplicationContextResources GetApplicationContextResources() {
            ApplicationContextResources applicationContextRessources = base.GetApplicationContextResources();
            applicationContextRessources.AddContextLocation(@"assembly://Spring.Attributes.Demo/Com.QueoFlow.Spring.Attributes.Demo.Config/Spring.XmlConfigDummy.xml");
            return applicationContextRessources;
        }

        protected override IList<Type> GetAttributeWhitelist() {
            return new List<Type>() { typeof(RepositoryAttribute) };
        }
    }
}