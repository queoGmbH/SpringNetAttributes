using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Spring.Context;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test {
    public class ResourceInjector {
        /// <summary>
        ///     Der Spring Application Context.
        /// </summary>
        private readonly IApplicationContext _context;

        public ResourceInjector(IApplicationContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        /// <summary>
        ///     Inject Spring Beans in every field with <see cref="ResourceAttribute" /> attribute.
        /// </summary>
        /// <param name="instance">the instance to scan</param>
        public void InjectRessources(Object instance) {
            if (instance == null) {
                throw new ArgumentNullException("instance");
            }

            foreach (FieldInfo field in FieldsWithRessourceAttribute(instance.GetType())) {
                InjectRessource(instance, field, GetRessourceAttribute(field));
            }
        }

        private static IEnumerable<FieldInfo> AllFields(Type type) {
            FieldInfo[] allFieldsOfSubclass =
                    type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            if (type.BaseType != null) {
                List<FieldInfo> withParent = new List<FieldInfo>();
                withParent.AddRange(FieldsWithRessourceAttribute(type.BaseType));
                withParent.AddRange(allFieldsOfSubclass);
                return withParent;
            } else {
                return allFieldsOfSubclass;
            }
        }

        private static String DescribeField(FieldInfo field) {
            return "field " + field.Name + " of class " + field.ReflectedType.Name;
        }

        private static String DetermineRessourceNameFromFieldName(FieldInfo field) {
            if (field.Name.StartsWith("_")) {
                return field.Name.Substring(1);
            } else {
                return field.Name;
            }
        }

        private static string DetermineRessourceType(FieldInfo field, ResourceAttribute fieldAttribute) {
            if (String.IsNullOrEmpty(fieldAttribute.Name)) {
                return DetermineRessourceNameFromFieldName(field);
            } else {
                return fieldAttribute.Name;
            }
        }

        private static IEnumerable<FieldInfo> FieldsWithRessourceAttribute(Type type) {
            return AllFields(type).Where(x => x.GetCustomAttributes(typeof(ResourceAttribute), false).Length > 0);
        }

        private static ResourceAttribute GetRessourceAttribute(FieldInfo fieldWithRessourceAttribute) {
            Object[] ressourceAttributes = fieldWithRessourceAttribute.GetCustomAttributes(typeof(ResourceAttribute), false);
            switch (ressourceAttributes.Count()) {
                case 0:
                    throw new ArgumentException("Field " + fieldWithRessourceAttribute + " is field wihtout RessourceAttribute");
                case 1:
                    return (ResourceAttribute)ressourceAttributes[0];
                default:
                    throw new Exception(DescribeField(fieldWithRessourceAttribute)
                                        + " has more than 1 RessourceAttribute. But only a unique attribute is supported.");
            }
        }

        private Object DetermineResource(FieldInfo field, ResourceAttribute fieldAttribute) {
            String ressourceName = DetermineRessourceType(field, fieldAttribute);
            ressourceName = char.ToUpper(ressourceName[0]) + ressourceName.Substring(1);
            string firstFullNameByFieldName = _context.GetObjectDefinitionNames().FirstOrDefault(x => x.EndsWith(ressourceName));
            if (!string.IsNullOrWhiteSpace(firstFullNameByFieldName)) {
                return _context.GetObject(firstFullNameByFieldName, field.FieldType);
            }
            return null;
        }

        private void InjectRessource(Object instance, FieldInfo field, ResourceAttribute fieldAttribute) {
            try {
                Object ressorce = DetermineResource(field, fieldAttribute);
                field.SetValue(instance, ressorce);
            } catch (Exception e) {
                throw new Exception("Error while injecting ressourse in " + DescribeField(field), e);
            }
        }
    }
}