﻿
using System;
using System.Configuration;
using System.IO;
using System.Timers;

namespace FliqloWPF
{
    public class AppViewModel : ObservableObject
    {
        readonly Timer timer;
        string minute;
        string hour;
        bool is12HourClock;
        bool isAM;
        double width;
        byte brightness;

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

        public AppViewModel()
        {
            Init();
            CalculateHourAndMinute();
            timer = new Timer(250);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Init()
        {
            Is12HourClock = Properties.Settings.Default.Is12HourClock;
            Brightness = Properties.Settings.Default.Brightness;
            Width = Properties.Settings.Default.Width;
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
            Properties.Settings.Default.Is12HourClock = Is12HourClock;
            Properties.Settings.Default.Brightness = Brightness;
            Properties.Settings.Default.Width = Width;
            Properties.Settings.Default.Save();
        }
    }
}