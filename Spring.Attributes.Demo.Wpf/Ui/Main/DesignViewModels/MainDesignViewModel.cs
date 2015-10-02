using ReactiveUI;

namespace Com.QueoFlow.Spring.Attributes.Demo.Wpf.Ui.Main.DesignViewModels {
    /// <summary>
    ///     Design View Model der Hauptansicht
    /// </summary>
    public class MainDesignViewModel :ReactiveObject, IMainViewModel {
        /// <summary>
        /// Die Methode in welcher alle wichtigen Daten für das ViewModel geladen werden sollten.
        ///             Diese Methode wird aufgerufen wenn eine View über einen Create/EditCommand angefordert wird.
        /// </summary>
        public void LoadData() {
            Content = "Design Time Content";
        }

        /// <summary>
        ///     Liefert den Inhalt des Fensters
        /// </summary>
        public string Content { get; private set; }
    }
}