using System;

using Spring.Objects.Factory.Config;

namespace Com.QueoFlow.Spring.Attributes {
    /// <summary>
    ///     Attribut um eine Klasse in den Spring Kontext aufzunehmen
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute {
        private AutoWiringMode _autoWiringMode = AutoWiringMode.No;

        public AutoWiringMode AutoWiringMode {
            get { return _autoWiringMode; }
            set { _autoWiringMode = value; }
        }

        public bool IsSingleton { get; set; }
    }
}