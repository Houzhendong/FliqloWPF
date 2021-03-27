using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FliqloWPF
{
    /// <summary>
    /// Test.xaml 的交互逻辑
    /// </summary>
    public partial class FlipNumber : UserControl
    {
        DoubleAnimation nextBottomHalfFlipAnimation;
        DoubleAnimation currentTopHalfFlipAnimation;
        Storyboard storyboard;

        public FlipNumber()
        {
            InitializeComponent();

            storyboard = new Storyboard();

            nextBottomHalfFlipAnimation = new DoubleAnimation
            {
                From = 0,
                To = 180,
                Duration = TimeSpan.FromSeconds(0.5),
            };
            currentTopHalfFlipAnimation = new DoubleAnimation
            {
                From = 0,
                To = 180,
                Duration = TimeSpan.FromSeconds(0.5),
            };

            Storyboard.SetTargetName(nextBottomHalfFlipAnimation, "axisNextBottomHalf");
            Storyboard.SetTargetName(currentTopHalfFlipAnimation, "axisCurrentTopHalf");
            Storyboard.SetTargetProperty(nextBottomHalfFlipAnimation, new PropertyPath("Angle"));
            Storyboard.SetTargetProperty(currentTopHalfFlipAnimation, new PropertyPath("Angle"));
            storyboard.Children.Add(nextBottomHalfFlipAnimation);
            storyboard.Children.Add(currentTopHalfFlipAnimation);

            storyboard.Completed += Storyboard_Completed;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            currentBottomPage.Visibility = Visibility.Collapsed;
            currentTopPage.Visibility = Visibility.Collapsed;
            nextBottomPage.Visibility = Visibility.Collapsed;
            LastNumber = CurrentNumber;
        }

        #region Dependency Property

        public string LastNumber
        {
            get { return (string)GetValue(CurrentNumberProperty); }
            set { SetValue(CurrentNumberProperty, value); }
        }

        public static readonly DependencyProperty CurrentNumberProperty =
            DependencyProperty.Register(nameof(LastNumber), typeof(string), typeof(FlipNumber), new PropertyMetadata(defaultValue: "0"));

        public string CurrentNumber
        {
            get { return (string)GetValue(NextNumberProperty); }
            set { SetValue(NextNumberProperty, value); }
        }

        public static readonly DependencyProperty NextNumberProperty =
            DependencyProperty.Register(nameof(CurrentNumber), typeof(string), typeof(FlipNumber), new PropertyMetadata("0", OnCurrentNumberChanged));

        public bool IsAM
        {
            get { return (bool)GetValue(IsAMProperty); }
            set { SetValue(IsAMProperty, value); }
        }

        public static readonly DependencyProperty IsAMProperty =
            DependencyProperty.Register(nameof(IsAM), typeof(bool), typeof(FlipNumber), new PropertyMetadata(false));

        public bool ShowMeridiemIndicator
        {
            get { return (bool)GetValue(ShowMeridiemIndicatorProperty); }
            set { SetValue(ShowMeridiemIndicatorProperty, value); }
        }

        public static readonly DependencyProperty ShowMeridiemIndicatorProperty =
            DependencyProperty.Register(nameof(ShowMeridiemIndicator), typeof(bool), typeof(FlipNumber), new PropertyMetadata(false));



        public Color Brightness
        {
            get { return (Color)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Brightness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrightnessProperty =
            DependencyProperty.Register(nameof(Brightness), typeof(Color), typeof(FlipNumber), new PropertyMetadata(default(Color)));

        #endregion
        private static void OnCurrentNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as FlipNumber).DoFlipAnimation();
        }

        public void DoFlipAnimation()
        {
            currentBottomPage.Visibility = Visibility.Visible;
            currentTopPage.Visibility = Visibility.Visible;
            nextBottomPage.Visibility = Visibility.Visible;
            storyboard.Begin(this);
        }

    }
}
