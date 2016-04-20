namespace Com.QueoFlow.Spring.Attributes
{
    public interface ISpringFactory
    {
        /// <summary>
        ///   Liefert ein konfiguriertes ViewModel des Typs zurück mit welcher die GetMethode aufgerufen wird. Die ViewModels werden mittels Spring konfiguriert.
        /// </summary>
        /// <typeparam name="T"> Der Typ des ViewModels </typeparam>
        /// <returns> Eine Instanz des ViewModelTyps </returns>
        T Get<T>(object argument = null);

        /// <summary>
        ///   Liefert ein konfiguriertes ViewModel des Typs zurück mit welcher die GetMethode aufgerufen wird. Die ViewModels werden mittels Spring konfiguriert.
        /// </summary>
        /// <typeparam name="T"> Der Typ des ViewModels </typeparam>
        /// <returns> Eine Instanz des ViewModelTyps </returns>
        T Get<T>(params object[] arguments);
    }
}