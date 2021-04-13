using System;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Threading;

namespace FliqloWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel();
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingPanel.Visibility = Visibility.Visible;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AppViewModel).SaveSetting();
            SettingPanel.Visibility = Visibility.Collapsed;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            var args = Environment.GetCommandLineArgs().Skip(1).ToArray();
            var arg = string.Join(string.Empty, args);
            if (arg.Contains("/c"))
                SettingButton.Visibility = Visibility.Visible;
            KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
