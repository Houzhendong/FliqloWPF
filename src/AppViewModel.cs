using System;
using System.Timers;
using Microsoft.Win32;

namespace FliqloWPF
{
    public class AppViewModel : ObservableObject
    {
        const string AppRegistryName = "FliqloWPF";
        const string Is12HourName = "Is12Hour";
        const string BrightnessName = "Brightness";
        const string WidthName = "Width";

        readonly Timer timer;
        string minute;
        string hour;
        bool is12HourClock;
        bool isAM;
        double width = 0.8f;
        byte brightness;
        static AppViewModel instance;

        public string Minute
        {
            get => minute;
            set => SetProperty(ref minute, value);
        }

        public string Hour
        {
            get => hour;
            set => SetProperty(ref hour, value);
        }

        public bool Is12HourClock
        {
            get => is12HourClock;
            set => SetProperty(ref is12HourClock, value);
        }

        public bool IsAM
        {
            get => isAM;
            set => SetProperty(ref isAM, value);
        }

        public double Width
        {
            get => width;
            set => SetProperty(ref width, value);
        }

        public byte Brightness
        {
            get => brightness;
            set => SetProperty(ref brightness, value);
        }

        public static AppViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppViewModel();
                    return instance;
                }
                return instance;
            }
        }

        public AppViewModel()
        {
            if (HaveRegistryKey())
            {
                LoadSettings();
            }
            else
            {
                InitRegistry();
            }
            CalculateHourAndMinute();
            timer = new Timer(250);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void LoadSettings()
        {
            RegistryKey localMechine = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            RegistryKey appKey = localMechine.OpenSubKey(AppRegistryName, true);

            Is12HourClock = bool.Parse(appKey.GetValue(Is12HourName).ToString());
            Brightness = byte.Parse(appKey.GetValue(BrightnessName).ToString());
            Width = double.Parse(appKey.GetValue(WidthName).ToString());

            appKey.Close();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CalculateHourAndMinute();
        }

        void CalculateHourAndMinute()
        {
            var m = DateTime.Now.Minute;
            var h = DateTime.Now.Hour;

            IsAM = h < 12;
            if (is12HourClock && h > 12)
                h %= 12;
            Minute = m.ToString("D2");
            Hour = h.ToString();
        }

        public void SaveSetting()
        {
            RegistryKey localMechine = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            RegistryKey appKey = localMechine.OpenSubKey(AppRegistryName, true);
            appKey.SetValue(Is12HourName, Is12HourClock);
            appKey.SetValue(BrightnessName, Brightness);
            appKey.SetValue(WidthName, Width);
            appKey.Close();
        }

        private void InitRegistry()
        {
            RegistryKey localMechine = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            RegistryKey appKey = localMechine.OpenSubKey(AppRegistryName, true);
            if (appKey == null)
            {
                appKey = localMechine.CreateSubKey(AppRegistryName);
            }
            appKey.SetValue(Is12HourName, Is12HourClock);
            appKey.SetValue(BrightnessName, Brightness);
            appKey.SetValue(WidthName, Width);
            appKey.Close();
        }

        private bool HaveRegistryKey()
        {
            RegistryKey localMechine = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            RegistryKey appKey = localMechine.OpenSubKey(AppRegistryName, true);
            bool result = appKey != null;

            appKey?.Close();

            return result;
        }
    }
}