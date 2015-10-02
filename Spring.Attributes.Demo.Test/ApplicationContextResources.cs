using System;
using System.Collections.Generic;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test {

    /// <summary>
    /// Die Klasse kapselt alle Ressourcen, die für die Konfiguration
    /// des ApplicationContexts benötig werden.
    /// </summary>
    public class ApplicationContextResources {
        private readonly List<string> _contextLocations = new List<string>();

        /// <summary>
        /// Fügt eine neue Ressource für die Konfiguration
        /// des ApplicationContexts hinzu.
        /// </summary>
        /// <param name="contextLocation"></param>
        public void AddContextLocation(string contextLocation) {
            if (string.IsNullOrEmpty(contextLocation)) {
                throw new ArgumentNullException("contextLocation");
            }
            if (!this._contextLocations.Contains(contextLocation)) {
                this._contextLocations.Add(contextLocation);
            }
        }

        /// <summary>
        /// Setzt ein neues Array mit den Ressourcen zur Konfiguration
        /// des ApplicationContexts.
        /// </summary>
        /// <param name="contextLocations"></param>
        public void SetContextLocations(string[] contextLocations) {
            this._contextLocations.Clear();
            this._contextLocations.AddRange(contextLocations);
        }

        /// <summary>
        /// Liefert ein Array mit den Konfigurationen des ApplicationContexts.
        /// </summary>
        public string[] ContextLocations {
            get { return this._contextLocations.ToArray(); }
        }
    }
}
