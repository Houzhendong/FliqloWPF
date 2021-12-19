using System;
using System.Timers;
using Gma.System.MouseKeyHook;
using Microsoft.Win32;

namespace FliqloWPF
{
    public class AppViewModel : ObservableObject
    {
        const string AppRegistryName = "FliqloWPF";
        const string Is12HourName = "Is12Hour";
        const string BrightnessName = "Brightness";
        const string WidthName = "Width";
        const string SpanName = "TriggerTimeSpan";
        const string s_registryStartupLocation = @"Software\Microsoft\Windows\CurrentVersion\Run";

        readonly Timer _timer;
        string _minute;
        string _hour;
        bool _is12HourClock;
        bool _isAM;
        double _width = 0.8f;
        byte _brightness;
        private readonly IKeyboardMouseEvents _globalHook;
        private long _lastTimeTick;
        private int _triggerTimespan = 1;
        private bool _startOnBoot;
        static readonly Lazy<AppViewModel> s_instance = new Lazy<AppViewModel>(() => new AppViewModel());

        public event EventHandler ShowWindowEvent;

        public string Minute
        {
            get => _minute;
            set => SetProperty(ref _minute, value);
        }

        public string Hour
        {
            get => _hour;
            set => SetProperty(ref _hour, value);
        }

        public bool Is12HourClock
        {
            get => _is12HourClock;
            set => SetProperty(ref _is12HourClock, value);
        }

        public bool IsAM
        {
            get => _isAM;
            set => SetProperty(ref _isAM, value);
        }

        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public byte Brightness
        {
            get => _brightness;
            set => SetProperty(ref _brightness, value);
        }

        public int TriggerTimespan
        {
            get => _triggerTimespan;
            set => SetProperty(ref _triggerTimespan, value);
        }

        public bool StartOnBoot
        {
            get => _startOnBoot;
            set
            {
                if (SetProperty(ref _startOnBoot, value))
                {
                    if (_startOnBoot)
                    {
                        SetStartOnBoot();
                    }
                    else
                    {
                        RemoveStartOnBoot();
                    }
                }
            }
        }

        public static AppViewModel Instance => s_instance.Value;

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
            _globalHook = Hook.GlobalEvents();
            _globalHook.MouseMove += OnMoveMove;
            _globalHook.KeyDown += OnKeyDown;
            CalculateHourAndMinute();
            _timer = new Timer(250);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _lastTimeTick = DateTime.Now.Ticks;
        }

        private void OnMoveMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _lastTimeTick = DateTime.Now.Ticks;
        }

        private void LoadSettings()
        {
            RegistryKey localMechine = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            RegistryKey appKey = localMechine.OpenSubKey(AppRegistryName, true);

            Is12HourClock = bool.Parse(appKey.GetValue(Is12HourName).ToString());
            Brightness = byte.Parse(appKey.GetValue(BrightnessName).ToString());
            Width = double.Parse(appKey.GetValue(WidthName).ToString());
            if (appKey.GetValue(SpanName) != null)
            {
                TriggerTimespan = int.Parse(appKey.GetValue(SpanName).ToString());
            }
            StartOnBoot = CheckStartOnBoot();

            appKey.Close();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CalculateHourAndMinute();
            CheckTimeSpan();
        }

        private void CheckTimeSpan()
        {
            TimeSpan span = TimeSpan.FromTicks(DateTime.Now.Ticks - _lastTimeTick);
            if (span > TimeSpan.FromMinutes(TriggerTimespan))
            {
                ShowWindowEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        void CalculateHourAndMinute()
        {
            int m = DateTime.Now.Minute;
            int h = DateTime.Now.Hour;

            IsAM = h < 12;
            if (_is12HourClock && h > 12)
            {
                h %= 12;
            }

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
            appKey.SetValue(SpanName, TriggerTimespan);
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
            appKey.SetValue(SpanName, TriggerTimespan);
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

        private bool CheckStartOnBoot()
        {
            RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(s_registryStartupLocation);
            bool startOnBoot = startupKey.GetValue(AppRegistryName) != null;
            startupKey.Close();
            return startOnBoot;
        }

        void SetStartOnBoot()
        {
            RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(s_registryStartupLocation, true);
            startupKey.SetValue(AppRegistryName, $"{System.Windows.Forms.Application.ExecutablePath}");
            startupKey.Close();
        }

        void RemoveStartOnBoot()
        {
            RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(s_registryStartupLocation, true);
            startupKey.DeleteValue(AppRegistryName);
            startupKey.Close();
        }
    }
}