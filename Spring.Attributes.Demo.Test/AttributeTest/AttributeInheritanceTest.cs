using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test.AttributeTest {
    [TestClass]
    public class AttributeInheritanceTest {
        [TestMethod]
        public void TestGetCustomAttributes() {
            MySuperAttribute[] attributes = (MySuperAttribute[])typeof(MyClass).GetCustomAttributes(typeof(MySuperAttribute), true);
            Assert.AreEqual(1, attributes.Length);
            Assert.IsTrue(attributes[0] is MySubAttribute);
        }

        [MySub]
        internal class MyClass {}

        internal class MySubAttribute:MySuperAttribute { }

        internal class MySuperAttribute:Attribute { }
    }
}
