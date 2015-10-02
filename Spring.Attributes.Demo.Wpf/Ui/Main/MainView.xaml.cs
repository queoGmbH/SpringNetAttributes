using System.ComponentModel;
using System.Windows;

namespace Com.QueoFlow.Spring.Attributes.Demo.Wpf.Ui.Main {
    /// <summary>
    ///     Interaktionslogik für MainView.xaml
    /// </summary>
    [Component]
    public partial class MainView : Window {
        /// <summary>
        /// </summary>
        public MainView() {
            InitializeComponent();
        }

        private void WindowClosing(object sender, CancelEventArgs e) {
            //if (DataContext is IMainViewModel) {
            //    IMainViewModel vm = DataContext as IMainViewModel;
            //    vm.ApplicationExitCommand.Execute(null);
            //}
        }
    }
}