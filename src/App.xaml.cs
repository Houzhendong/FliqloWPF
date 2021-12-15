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
            System.Diagnostics.Debug.WriteLine($"\r\n{System.Windows.Forms.Screen.PrimaryScreen.WorkingArea}\r\n");
            if (System.Windows.Forms.SystemInformation.MonitorCount > 1)
            {
                System.Drawing.Rectangle workingArea = System.Windows.Forms.Screen.AllScreens.Where(s => !s.Primary).FirstOrDefault().WorkingArea;
                MainWindow secondWindow = new MainWindow
                {
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Top = workingArea.Top,
                    Left = workingArea.Left
                };
                secondWindow.Show();
            }
        }
    }
}
