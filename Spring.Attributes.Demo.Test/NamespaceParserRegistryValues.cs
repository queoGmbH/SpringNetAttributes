using System;

using Spring.Data.Config;
using Spring.Transaction.Config;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test {
    /// <summary>
    /// Eine (nicht vollständige) Auswahl an
    /// Namespace Parsern.
    /// </summary>
    public enum NamespaceParserRegistryValues {
        DatabaseNamespaceParser,
        TxNamespaceParser
    }

    public static class NamespaceParserRegistryValuesExtension {
        public static Type GetValue(this NamespaceParserRegistryValues value) {
            Type resultType = null;
            switch (value) {
                case NamespaceParserRegistryValues.DatabaseNamespaceParser:
                    resultType = typeof(DatabaseNamespaceParser);
                    break;
                case NamespaceParserRegistryValues.TxNamespaceParser:
                    resultType = typeof(TxNamespaceParser);
                    break;
            }
            return resultType;
        }
    }
}