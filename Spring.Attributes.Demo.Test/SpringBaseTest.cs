using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spring.Context;
using Spring.Objects.Factory.Xml;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test {
    /// <summary>
    ///     Der SpringBaseTest stellt automatisch einen konfigurierten Spring Ioc Container zur Verfügung.
    ///     Es stehen einige Methoden zur Verfügung um in die Konfiguration einzugreifen.
    ///     Eingebunden ist außerdem der ResourceInjector ...
    ///     <see cref="ResourceInjector" />
    /// </summary>
    [TestClass]
    public abstract class SpringBaseTest {
        private IApplicationContext _context;

        /// <summary>
        ///     Der Standardkonstruktor.
        ///     Registriert die Namespaceparser, DB-Providernamen, Erzeugt den Kontext und injiziert die Resourcen die mit dem
        ///     Resource-Attribut gekennzeichnet sind.
        /// </summary>
        protected SpringBaseTest() {
            RegisterParsers();
            CreateContext();
            new ResourceInjector(Context).InjectRessources(this);
        }

        /// <summary>
        ///     Liefert den IApplicationContext vom Spring IoC Container.
        /// </summary>
        protected IApplicationContext Context {
            get { return _context; }
        }

        /// <summary>
        ///     Diese Methode Säubert nach den Tests die Testklasse.
        /// </summary>
        [TestCleanup]
        public virtual void CleanupTest() {
        }

        /// <summary>
        ///     Diese Methode Initialisiert die Testklasse.
        /// </summary>
        [TestInitialize]
        public virtual void InitializeTest() {
        }

        /// <summary>
        ///     Liefert die Resourcen die zum ApplicationCoontext gehören.
        ///     Diese Methode muss überschrieben werden, wenn weitere Konfigurationsdateinen eingebunden werden sollen.
        /// </summary>
        /// <returns></returns>
        protected virtual ApplicationContextResources GetApplicationContextResources() {
            return new ApplicationContextResources();
        }

        protected abstract IList<Type> GetAttributeWhitelist();

        /// <summary>
        ///     Diese Methode liefert die NamespaceparserregistryConfiguration und muss überschrieben werden wenn neue
        ///     Namespaceparser hinzugefügt werden sollen.
        /// </summary>
        /// <returns></returns>
        protected virtual NamespaceParserRegistryConfiguration GetNamespaceParserRegistryConfiguration() {
            NamespaceParserRegistryConfiguration nprc = new NamespaceParserRegistryConfiguration();
            nprc.AddParser(NamespaceParserRegistryValues.DatabaseNamespaceParser);

            return nprc;
        }

        private void CreateContext() {
            try {
                ApplicationContextResources applicationContextResources = GetApplicationContextResources();
                IList<Type> attributeWhitelist = GetAttributeWhitelist();
                MixedApplicationContext mixedApplicationContext = new MixedApplicationContext(attributeWhitelist,
                        true,
                        applicationContextResources.ContextLocations);
                _context = mixedApplicationContext;
            } catch (Exception exception) {
                throw new ApplicationException(exception.Message + exception.StackTrace);
            }
        }

        private void RegisterParsers() {
            NamespaceParserRegistryConfiguration configuration = GetNamespaceParserRegistryConfiguration();
            foreach (Type type in configuration.ParserTypes) {
                NamespaceParserRegistry.RegisterParser(type);
            }
        }
    }
}