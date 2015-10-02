using System;
using System.Collections.Generic;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test {
    /// <summary>
    /// Die Klasse verwaltet alle NamespaceParser die für die 
    /// Konfiguration des ApplicationContexts benötigt werden.
    /// </summary>
    public class NamespaceParserRegistryConfiguration {
        private readonly List<Type> _parserTypes = new List<Type>();

        /// <summary>
        /// Fügt einen NamespaceParser hinzu.
        /// </summary>
        /// <param name="parserType">Typ des Parsers</param>
        public void AddParserType(Type parserType) {
            if (!this._parserTypes.Contains(parserType)) {
                this._parserTypes.Add(parserType);
            }
        }

        /// <summary>
        /// Entfernt einen NamespaceParser aus der Konfiguration.
        /// </summary>
        /// <param name="parserType">Typ des Parsers.</param>
        public void RemoveParserType(Type parserType) {
            if (this._parserTypes.Contains(parserType)) {
                this._parserTypes.Remove(parserType);
            }
        }

        /// <summary>
        /// Liefert eine Liste mit allen registrierten Parsern.
        /// </summary>
        public IList<Type> ParserTypes {
            get { return this._parserTypes; }
        }

        /// <summary>
        /// Fügt der Konfiguration einen NamespaceParser anhand des
        /// Enum Wertes hinzu.
        /// </summary>
        /// <param name="parser"></param>
        public void AddParser(NamespaceParserRegistryValues parser) {
            Type parserType = parser.GetValue();
            this.AddParserType(parserType);
        }

        /// <summary>
        /// Entfernt einen NamespaceParser aus der Konfiguration.
        /// </summary>
        /// <param name="parser"></param>
        public void RemoveParser(NamespaceParserRegistryValues parser) {
            Type parserType = parser.GetValue();
            this.RemoveParserType(parserType);
        }
    }
}
