using System;
using System.Collections.Generic;

using Com.QueoFlow.Spring.Attributes.Demo.Test.Service;

namespace Com.QueoFlow.Spring.Attributes.Demo.Wpf.Test.ViewModels {
    public class ViewModelTestBase : ServiceTestBase {
        protected override IList<Type> GetAttributeWhitelist() {
            return new List<Type>() { typeof(ViewModelAttribute), typeof(RepositoryAttribute) };
        }
    }
}