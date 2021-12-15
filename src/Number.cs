using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace FliqloWPF
{
    public class Number : UserControl
    {
        DoubleAnimation _nextBottomHalfFlipAnimation;
        DoubleAnimation _currentTopHalfFlipAnimation;
        Storyboard _storyboard;
        private bool _isAnimating;
        Viewbox _currentBottomPage;
        Viewbox _currentTopPage;
        Viewbox _nextBottomPage;
        AxisAngleRotation3D _axisNextBottomHalf;
        AxisAngleRotation3D _axisCurrentTopHalf;

        public Number()
        {
            DefaultStyleKey = typeof(Number);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _currentTopPage = GetTemplateChild("currentTopPage") as Viewbox;
            _currentBottomPage = GetTemplateChild("currentBottomPage") as Viewbox;
            _nextBottomPage = GetTemplateChild("nextBottomPage") as Viewbox;
            _axisNextBottomHalf = GetTemplateChild("axisNextBottomHalf") as AxisAngleRotation3D;
            _axisCurrentTopHalf = GetTemplateChild("axisCurrentTopHalf") as AxisAngleRotation3D;

            _storyboard = new Storyboard();

            _nextBottomHalfFlipAnimation = new DoubleAnimation
            {
                From = 0,
                To = 180,
                Duration = TimeSpan.FromSeconds(0.5),
            };
            _currentTopHalfFlipAnimation = new DoubleAnimation
            {
                From = 0,
                To = 180,
                Duration = TimeSpan.FromSeconds(0.5),
            };

            Storyboard.SetTarget(_nextBottomHalfFlipAnimation, _axisNextBottomHalf);
            Storyboard.SetTarget(_currentTopHalfFlipAnimation, _axisCurrentTopHalf);
            Storyboard.SetTargetProperty(_nextBottomHalfFlipAnimation, new PropertyPath("Angle"));
            Storyboard.SetTargetProperty(_currentTopHalfFlipAnimation, new PropertyPath("Angle"));
            _storyboard.Children.Add(_nextBottomHalfFlipAnimation);
            _storyboard.Children.Add(_currentTopHalfFlipAnimation);

            _storyboard.Completed += Storyboard_Completed;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            _currentBottomPage.Visibility = Visibility.Collapsed;
            _currentTopPage.Visibility = Visibility.Collapsed;
            _nextBottomPage.Visibility = Visibility.Collapsed;
            LastNumber = CurrentNumber;
            _isAnimating = false;
        }

        #region Dependency Property

        public string LastNumber
        {
            get { return (string)GetValue(CurrentNumberProperty); }
            set { SetValue(CurrentNumberProperty, value); }
        }

        public static readonly DependencyProperty CurrentNumberProperty =
            DependencyProperty.Register(nameof(LastNumber), typeof(string), typeof(Number), new PropertyMetadata(defaultValue: "0"));

        public string CurrentNumber
        {
            get { return (string)GetValue(NextNumberProperty); }
            set { SetValue(NextNumberProperty, value); }
        }

        public static readonly DependencyProperty NextNumberProperty =
            DependencyProperty.Register(nameof(CurrentNumber), typeof(string), typeof(Number), new PropertyMetadata("0", OnCurrentNumberChanged));

        public bool IsAM
        {
            get { return (bool)GetValue(IsAMProperty); }
            set { SetValue(IsAMProperty, value); }
        }

        public static readonly DependencyProperty IsAMProperty =
            DependencyProperty.Register(nameof(IsAM), typeof(bool), typeof(Number), new PropertyMetadata(false));

        public bool ShowMeridiemIndicator
        {
            get { return (bool)GetValue(ShowMeridiemIndicatorProperty); }
            set { SetValue(ShowMeridiemIndicatorProperty, value); }
        }

        public static readonly DependencyProperty ShowMeridiemIndicatorProperty =
            DependencyProperty.Register(nameof(ShowMeridiemIndicator), typeof(bool), typeof(Number), new PropertyMetadata(false));



        public Color Brightness
        {
            get { return (Color)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }

        public static readonly DependencyProperty BrightnessProperty =
            DependencyProperty.Register(nameof(Brightness), typeof(Color), typeof(Number), new PropertyMetadata(default(Color)));

        #endregion
        private static void OnCurrentNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Number).DoFlipAnimation();
        }

        public void DoFlipAnimation()
        {
            if (_currentBottomPage == null || _currentTopPage == null || _nextBottomPage == null)
            {
                return;
            }

            if (_isAnimating)
            {
                return;
            }

            _currentBottomPage.Visibility = Visibility.Visible;
            _currentTopPage.Visibility = Visibility.Visible;
            _nextBottomPage.Visibility = Visibility.Visible;
            _storyboard.Begin(this);

            _isAnimating = true;
        }

    }
}
