using System;
using System.Linq;
using System.Windows;

namespace FliqloWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppViewModel ViewModel => DataContext as AppViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = AppViewModel.Instance;
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            SettingPanel.Visibility = SettingPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AppViewModel).SaveSetting();
            SettingPanel.Visibility = Visibility.Collapsed;
            SettingButton.Visibility = Visibility.Collapsed;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            string[] args = Environment.GetCommandLineArgs().Skip(1).ToArray();
            string arg = string.Join(string.Empty, args);
            //if (arg.Contains("/c"))
            //{
            //    SettingButton.Visibility = Visibility.Visible;
            //}
            AppViewModel.Instance.ShowWindowEvent += ShowWindowEventHandler;
            KeyDown += MainWindow_KeyDown;
        }

        private async void ShowWindowEventHandler(object sender, EventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                foreach (Window item in Application.Current.Windows)
                {
                    item.Show();
                }
            });
        }

        private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.LeftCtrl)
            {
                if (SettingButton.Visibility != Visibility.Visible)
                {
                    SettingButton.Visibility = Visibility.Visible;
                }
                return;
            }

            foreach (Window item in Application.Current.Windows)
            {
                SettingButton.Visibility = Visibility.Collapsed;
                item.Hide();
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            ViewModel.TriggerTimespan++;
        }

        private void MinusClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.TriggerTimespan > 1)
            {
                ViewModel.TriggerTimespan--;
            }
        }
    }
}
