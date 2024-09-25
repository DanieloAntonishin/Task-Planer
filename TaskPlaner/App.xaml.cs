using System.Windows;
using TaskPlaner.View;
using TaskPlaner.ViewModel;

namespace TaskPlaner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow _MainWindow;

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _MainWindow = new MainWindow();
            _MainWindow.Show();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            (_MainWindow.DataContext as MainViewModel).Dispose();
        }
    }
}