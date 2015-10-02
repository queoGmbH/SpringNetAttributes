using System;

using JetBrains.Annotations;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test {
    /// <summary>
    ///   Attribut zur Kennzeichnung von Felder die über Spring injiziert werden.
    /// </summary>
    [MeansImplicitUse]
    public class ResourceAttribute : Attribute {
        /// <summary>
        ///   Ruft den Namen der Spring Bean ab oder legt diesen fest.
        /// </summary>
        public String Name { get; set; }
    }
}