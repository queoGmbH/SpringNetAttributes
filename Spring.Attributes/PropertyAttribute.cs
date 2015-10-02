using System;

namespace Com.QueoFlow.Spring.Attributes {
    /// <summary>
    ///     Attribute, welches über ein Property geschrieben werden kann um einen bestimmten Typen zu injecten
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAttribute : Attribute {
        private readonly Type _typeToInject;

        /// <summary>
        ///     Initialisiert eine neue Instanz der <see cref="T:System.Attribute" />-Klasse.
        /// </summary>
        public PropertyAttribute(Type typeToInject) {
            _typeToInject = typeToInject;
        }

        /// <summary>
        ///     Liefert den Typen, der beim entsprechenden Property Injected werden soll
        /// </summary>
        public Type TypeToInject {
            get { return _typeToInject; }
        }
    }
}