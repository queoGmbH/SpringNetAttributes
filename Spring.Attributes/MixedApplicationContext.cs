﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Spring.Context.Support;
using Spring.Objects.Factory.Support;

namespace Com.QueoFlow.Spring.Attributes {
    /// <summary>
    ///     Spring.net Application Context, der Attribute auswertet aber auch XNL Konfigurationsdateien berücksichtigt.
    /// </summary>
    public class MixedApplicationContext : XmlApplicationContext {
        private static readonly IList<string> _assemblyNameBlacklist = new List<string>()
        { "mscorlib", "nCrunch", "NCrunch", "system", "System", "Presentation", "Microsoft" };

        /// <summary>
        ///     Creates a new instance of the
        ///     <see cref="T:Spring.Context.Support.XmlApplicationContext" /> class,
        ///     loading the definitions from the supplied XML resource locations.
        /// </summary>
        /// <remarks>
        ///     The created context will be case sensitive.
        /// </remarks>
        /// <param name="attributeWhitelist"></param>
        /// <param name="skippContextRegistration"></param>
        /// <param name="configurationLocations">
        ///     Any number of XML based object definition resource locations.
        /// </param>
        public MixedApplicationContext(IList<Type> attributeWhitelist, bool skippContextRegistration = false,
                params string[] configurationLocations)
                : base(configurationLocations) {
            /* -- Aus allen Assemblies die Typen suchen, die ein [Component] Attribut haben -- */
            IList<Tuple<Type, ComponentAttribute>> typesWithComponentTypeAttribute = new List<Tuple<Type, ComponentAttribute>>();

            /* Als Ausgangsmenge alle geladenen Assemblies (außer Blacklist) zur Menge der zu durchsuchenden hinzufügen */
            IList<Assembly> assembliesToSearchForAttributes =
                    AppDomain.CurrentDomain.GetAssemblies()
                            .Where(x => !_assemblyNameBlacklist.Any(y => x.FullName.StartsWith(y)))
                            .Distinct()
                            .ToList();

            /* Und anschließend alle in den Ursprungsassemblies referenzierten Assemblies rekursiv zur Liste der
             * zu durchsuchenden Assemblies hinzufügen
             * Außer jedoch Blacklisteinträge.*/
            FindReferencedAssembliesRecursive(null, assembliesToSearchForAttributes, _assemblyNameBlacklist);

            /* Anschließend die Assemblies nach Typen mit Component Attribute durchsuchen */
            Parallel.ForEach(assembliesToSearchForAttributes,
                    assembly => GetTypesWithComponentAttribute(assembly, typesWithComponentTypeAttribute, attributeWhitelist));

            /* -- Alle gefundenen Typen dem Kontext hinzufügen -- */
            Parallel.ForEach(typesWithComponentTypeAttribute,
                    type => {
                        IObjectDefinitionFactory objectDefinitionFactory = new DefaultObjectDefinitionFactory();
                        ObjectDefinitionBuilder builder = ObjectDefinitionBuilder.RootObjectDefinition(objectDefinitionFactory, type.Item1);
                        builder.SetAutowireMode(type.Item2.AutoWiringMode);
                        builder.SetSingleton(type.Item2.IsSingleton);
                        IList<PropertyInfo> properties = type.Item1.GetProperties();
                        foreach (PropertyInfo propertyInfo in properties) {
                            IList<PropertyAttribute> customAttributes =
                                    propertyInfo.GetCustomAttributes(typeof(PropertyAttribute), true).Cast<PropertyAttribute>().ToList();
                            if (customAttributes.Any()) {
                                builder.AddPropertyReference(propertyInfo.Name, customAttributes.First().TypeToInject.FullName);
                            }
                        }
                        ObjectFactory.RegisterObjectDefinition(type.Item1.FullName, builder.ObjectDefinition);
                    });

            /* -- Wenn gewünscht den Kontext registrieren -- */
            if (!skippContextRegistration) {
                ContextRegistry.RegisterContext(this);
            }
        }

        /// <summary>
        ///     Liefert die bei der Contexterzeugung verwendete Blacklist für Assemblynamen.
        /// </summary>
        public static IList<string> AssemblyNameBlacklist {
            get { return _assemblyNameBlacklist; }
        }

        /// <summary>
        ///     Erstellt einen Mixed Application Context und registriert diesen in der Context Registry.
        /// </summary>
        /// <param name="additionalConfigurations"></param>
        /// <returns></returns>
        public static MixedApplicationContext Create(IList<FileInfo> additionalConfigurations = null) {
            /* Config Dummy oder zusätzliche XML Konfigurationen hinzufügen.
             * Der Dummy wird für den korrekten Betrieb des Xml Context benötigt, da dieser 
             * mindestens eine Konfigdatei verlangt. */
            List<string> configurationFilesLocations = new List<string>();
            if (additionalConfigurations != null) {
                configurationFilesLocations.AddRange(additionalConfigurations.Select(x => x.FullName));
            } else {
                configurationFilesLocations.Add(@"assembly://Spring.Attributes.Demo/Com.QueoFlow.Spring.Attributes.Demo.Config/Spring.XmlConfigDummy.xml");
            }
            return new MixedApplicationContext(null, configurationLocations: configurationFilesLocations.ToArray());
        }

        /// <summary>
        ///     Sucht aus der übergebenen Assembly alle Typen mit dem
        ///     <see cref="Com.QueoFlow.JiraDesk.Attributes.ComponentAttribute" />
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="typesWithComponentTypeAttribute"></param>
        /// <param name="attributeWhitelist"></param>
        /// <returns></returns>
        private static void GetTypesWithComponentAttribute(Assembly assembly,
                IList<Tuple<Type, ComponentAttribute>> typesWithComponentTypeAttribute, IList<Type> attributeWhitelist) {
            if (assembly == null) {
                throw new ArgumentNullException("assembly");
            }
            Parallel.ForEach(assembly.GetTypes(),
                    type => {
                        ComponentAttribute[] allAttributesOnType =
                                (ComponentAttribute[])type.GetCustomAttributes(typeof(ComponentAttribute), true);
                        if (allAttributesOnType.Length > 0) {
                            ComponentAttribute firstAttribute = allAttributesOnType.First();
                            if (attributeWhitelist != null) {
                                /* Ist eine Whitelist aktiv, erst prüfen, ob der eigentliche Typ des Attributes in der Liste enthalten ist. Erst dann den Typen hinzufügen */
                                if (attributeWhitelist.Any(x => firstAttribute.GetType() == x)) {
                                    typesWithComponentTypeAttribute.Add(new Tuple<Type, ComponentAttribute>(type, firstAttribute));
                                }
                            } else {
                                /* Ist keine Whitelist aktiv, den Typen auf jeden Fall hinzufügen */
                                typesWithComponentTypeAttribute.Add(new Tuple<Type, ComponentAttribute>(type, firstAttribute));
                            }
                        }
                    });
        }

        private void FindReferencedAssembliesRecursive(Assembly assembly, IList<Assembly> assembliesToSearchForAttributes,
                IList<string> assemblyNameBlacklist) {
            if (assembly != null) {
                /* Rekursiver aufruf. Hier die Direkten Abhängigkeiten durchlaufen */
                IList<Assembly> dependencies = assembly.GetReferencedAssemblies().Select(Assembly.Load).ToList();
                foreach (Assembly dependency in dependencies) {
                    if (!assembliesToSearchForAttributes.Contains(dependency)
                        && !_assemblyNameBlacklist.Any(x => dependency.FullName.StartsWith(x))) {
                        assembliesToSearchForAttributes.Add(dependency);
                        FindReferencedAssembliesRecursive(dependency, assembliesToSearchForAttributes, assemblyNameBlacklist);
                    }
                }
            } else {
                /* Initialer Aufruf. Hier die Ausgangsmenge durchlaufen */
                IList<Assembly> copyOfOriginAssemblies = assembliesToSearchForAttributes.ToList();
                foreach (Assembly assymblyToScanDependencies in copyOfOriginAssemblies) {
                    FindReferencedAssembliesRecursive(assymblyToScanDependencies, assembliesToSearchForAttributes, assemblyNameBlacklist);
                }
            }
        }
    }
}