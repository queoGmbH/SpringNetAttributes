namespace Com.QueoFlow.Spring.Attributes.Demo.Persistence {
    /// <summary>
    ///     Schnittstelle für ein Datenrepository
    /// </summary>
    public interface IDataRepository {
        /// <summary>
        ///     Liefert einen Text
        /// </summary>
        /// <returns></returns>
        string GetText();
    }
}