using System.Windows;
using Com.QueoFlow.Spring.Attributes.Demo.Wpf.Ui.Main;
using Com.QueoFlow.Spring.Attributes.Demo.Wpf.Ui.Main.ViewModels;

namespace Com.QueoFlow.Spring.Attributes.Demo.Wpf {
    /// <summary>
    ///     Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App {
        private void ApplicationStartup(object sender, StartupEventArgs e) {
            MixedApplicationContext context = MixedApplicationContext.Create();

            MainView mainView = context.Get<MainView>();
            MainViewModel mainViewModel = context.Get<MainViewModel>();

            mainView.DataContext = mainViewModel;

            MainWindow = mainView;
            MainWindow.Show();

            mainViewModel.LoadData();

            MainWindow.Show();
        }
    }
}