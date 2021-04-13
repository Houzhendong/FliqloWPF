using System;
using System.Linq;
using System.Windows;

namespace FliqloWPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RunAtSecondScreen();
        }

        private void RunAtSecondScreen()
        {
            if(System.Windows.Forms.SystemInformation.MonitorCount > 1)
            {
                var workingArea = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault().WorkingArea;
                var secondWindow = new MainWindow();
                secondWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                secondWindow.Top = workingArea.Top;
                secondWindow.Left = workingArea.Left;
                secondWindow.Show();
            }
        }
    }
}
