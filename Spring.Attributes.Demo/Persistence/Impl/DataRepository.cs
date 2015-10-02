

namespace Com.QueoFlow.Spring.Attributes.Demo.Persistence.Impl {
    /// <summary>
    ///     Repository
    /// </summary>
    [Repository(IsSingleton = true)]
    public class DataRepository : IDataRepository {
        /// <summary>
        ///     Liefert einen Text
        /// </summary>
        /// <returns></returns>
        public string GetText() {
            return "The Answer is 42";
        }
    }
}