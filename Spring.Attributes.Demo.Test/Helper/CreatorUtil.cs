using Com.QueoFlow.Spring.Attributes.Demo.Persistence.Impl;

namespace Com.QueoFlow.Spring.Attributes.Demo.Test.Helper {
    /// <summary>
    ///     Tool das Methoden zum Erstellen von Domainobjekten bereitstellt.
    /// </summary>
    [Repository]
    public class CreatorUtil {
        private readonly DataRepository _dataRepository;

        /// <summary>
        ///     Erstellt eine Instanz der CreatorUtil-Klasse.
        /// </summary>
        public CreatorUtil(DataRepository dataRepository) {
            _dataRepository = dataRepository;
        }

        public DataRepository DataRepository {
            get { return _dataRepository; }
        }
    }
}