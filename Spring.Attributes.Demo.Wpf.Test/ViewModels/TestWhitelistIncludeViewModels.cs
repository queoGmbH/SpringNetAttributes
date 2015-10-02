using Com.QueoFlow.Spring.Attributes.Demo.Test;
using Com.QueoFlow.Spring.Attributes.Demo.Test.Helper;
using Com.QueoFlow.Spring.Attributes.Demo.Test.Persistence;
using Com.QueoFlow.Spring.Attributes.Demo.Wpf.Ui.Main.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.QueoFlow.Spring.Attributes.Demo.Wpf.Test.ViewModels {
    [TestClass]
    public class TestWhitelistIncludeViewModels : ViewModelTestBase {
        [Resource]
        private CreatorUtil _creatorUtil;

        [Resource]
        private MainViewModel _mainViewModel;

        /// <summary>
        ///     Testet das Injecten von Resourcen in diese Testklasse
        /// </summary>
        [TestMethod]
        public void TestResourceInjection() {
            Assert.IsNotNull(_creatorUtil);
            Assert.IsNotNull(_mainViewModel);
        }
    }

    [TestClass]
    public class TestWhitelistExludeViewModels : PersistenceBaseTest {
        [Resource]
        private CreatorUtil _creatorUtil;

        [Resource]
        private MainViewModel _mainViewModel;

        /// <summary>
        ///     Testet das Injecten von Resourcen in diese Testklasse
        /// </summary>
        [TestMethod]
        public void TestResourceInjection() {
            Assert.IsNotNull(_creatorUtil);
            Assert.IsNull(_mainViewModel);
        }
    }
}