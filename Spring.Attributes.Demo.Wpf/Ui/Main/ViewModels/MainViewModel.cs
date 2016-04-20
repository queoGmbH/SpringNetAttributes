using Com.QueoFlow.Spring.Attributes.Demo.Persistence;
using Com.QueoFlow.Spring.Attributes.Demo.Persistence.Impl;
using ReactiveUI;
using Spring.Objects.Factory.Config;

namespace Com.QueoFlow.Spring.Attributes.Demo.Wpf.Ui.Main.ViewModels
{
    /// <summary>
    ///     View Model eines Hauptfensters.
    /// </summary>
    [ViewModel(AutoWiringMode = AutoWiringMode.No)]
    public class MainViewModel : ReactiveObject, IMainViewModel
    {
        private string _content;

        /// <summary>
        ///     Setzt ein <see cref="IDataRepository" />
        /// </summary>
        [Property(typeof(DataRepository))]
        public IDataRepository DataRepository { set; private get; }

        /// <summary>
        ///     Setzt eine <see cref="ISpringFactory" />
        /// </summary>
        public ISpringFactory SpringFactory { set; private get; }

        /// <summary>
        ///     Liefert den Inhalt des Fensters
        /// </summary>
        public string Content
        {
            get { return _content; }
            private set { this.RaiseAndSetIfChanged(ref _content, value); }
        }

        /// <summary>
        ///     Die Methode in welcher alle wichtigen Daten für das ViewModel geladen werden sollten.
        ///     Diese Methode wird aufgerufen wenn eine View über einen Create/EditCommand angefordert wird.
        /// </summary>
        public void LoadData()
        {
            Content = DataRepository.GetText();

            Content = SpringFactory.Get<IDataRepository>().GetText();
        }
    }
}