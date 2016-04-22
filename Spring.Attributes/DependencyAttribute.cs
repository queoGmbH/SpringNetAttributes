using System;

namespace Com.QueoFlow.Spring.Attributes {
    /// <summary>
    ///     Attribute, welches über ein Property geschrieben werden kann um einen bestimmten Typen zu injecten
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DependencyAttribute : Attribute {
        private readonly Type _typeToInject;

        /// <summary>
        ///     Liefert eine neue Instanz des Attributs mit einem expliziten Typwunsch
        /// </summary>
        public DependencyAttribute(Type typeToInject) {
            _typeToInject = typeToInject;
        }

        /// <summary>
        ///     Liefert eine neue Instanz des Attributs ohne einen expliziten Typwunsch
        /// </summary>
        public DependencyAttribute() {
        }

        /// <summary>
        ///     Liefert den Typen, der beim entsprechenden Property Injected werden soll
        /// </summary>
        public Type TypeToInject
        {
            get { return _typeToInject; }
        }
    }
}