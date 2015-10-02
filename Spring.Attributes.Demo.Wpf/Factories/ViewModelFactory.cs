
using ReactiveUI;

using Spring.Context;
using Spring.Context.Support;

namespace Com.QueoFlow.Spring.Attributes.Demo.Wpf.Factories {
    /// <summary>
    /// Factory Klasse zur Erzegung der View Models.
    /// </summary>
    public static class ViewModelFactory {
        /// <summary>
        ///   Liefert ein konfiguriertes ViewModel des Typs zur�ck mit welcher die GetMethode aufgerufen wird. Die ViewModels werden mittels Spring konfiguriert.
        /// </summary>
        /// <typeparam name="T"> Der Typ des ViewModels </typeparam>
        /// <returns> Eine Instanz des ViewModelTyps </returns>
        public static T Get<T>(object argument = null) where T : ReactiveObject {
            object[] arguments = new object[] { };
            if (argument != null) {
                arguments = new[] { argument };
            }
            return Get<T>(arguments);
        }

        /// <summary>
        ///   Liefert ein konfiguriertes ViewModel des Typs zur�ck mit welcher die GetMethode aufgerufen wird. Die ViewModels werden mittels Spring konfiguriert.
        /// </summary>
        /// <typeparam name="T"> Der Typ des ViewModels </typeparam>
        /// <returns> Eine Instanz des ViewModelTyps </returns>
        public static T Get<T>(params object[] arguments) where T : ReactiveObject {
            IApplicationContext context = ContextRegistry.GetContext();
            T vm = context.GetObject(typeof(T).FullName, arguments) as T;
            return vm;
        }

    }
}