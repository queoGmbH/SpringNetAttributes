using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.QueoFlow.Spring.Attributes.Demo.Wpf.Test.ViewModels {
    [TestClass]
    public class CreatorUtilTest : ViewModelTestBase {
        /// <summary>
        ///     Testet ob die Konstruktor Injizierung korrekt funktioniert
        /// </summary>
        [TestMethod]
        public void TestCreaturUtilConstructorInjection() {
            Assert.IsNotNull(DomainObjectCreatorUtil.DataRepository);
        }

        /// <summary>
        ///     Testet ob das Creator Util vorhanden ist
        /// </summary>
        [TestMethod]
        public void TestHasCreatorUtil() {
            Assert.IsNotNull(DomainObjectCreatorUtil);
        }
    }
}