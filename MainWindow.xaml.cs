using JC_Click_V1.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace JC_Click_V1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String[] colors = { "#0000ff", "#ff0000", "#964B00", "#ffffff" };
        StringTexts stringText = new StringTexts();
        GetImages pathImages = new GetImages();
        ImageBrush changeImageBrush = new ImageBrush();
        SoundEffects sfxKeyboard = new SoundEffects();

        KeyboardHook keyboardHook;

        public MainWindow()
        {
            InitializeComponent();

            Txb_Descrition.Text = stringText.getdefaultScrpt();

            keyboardHook = new KeyboardHook();
            keyboardHook.KeyPressed += KeyboardHook_KeyPressed;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MyNotifyIcon.TrayMouseDoubleClick += MyNotifyIcon_TrayMouseDoubleClick;
        }

        private void MyNotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            // N: Handle double-click on the tray icon (show/hide the window, for example)
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
                Activate();
            }
            else
            {
                WindowState = WindowState.Minimized;
            }
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn_DefaultSwitch_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colors[0]);

            Lbl_DscrptHeader.Content = "Blue Switch";
            Lbl_DscrptHeader.Foreground = new SolidColorBrush(color);

            Btn_BlueSwitch.Visibility = Visibility.Visible;
            Btn_DefaultSwitch.Visibility = Visibility.Hidden;

            Txb_Descrition.Text = stringText.getBlueDscrpt();

            // N: Grid BLue Visibility True
            Grid_Blue_Selection.Visibility = Visibility.Visible; Grid_Red_Selection.Visibility = Visibility.Hidden; Grid_Brown_Selection.Visibility = Visibility.Hidden;

        }

        private void Btn_BlueSwitch_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colors[1]);

            Lbl_DscrptHeader.Content = "Red Switch";
            Lbl_DscrptHeader.Foreground = new SolidColorBrush(color);

            Btn_RedSwitch.Visibility = Visibility.Visible;
            Btn_BlueSwitch.Visibility = Visibility.Hidden;

            Txb_Descrition.Text = stringText.getRedDscrpt();

            // N: Grid Red Visibility True
            Grid_Red_Selection.Visibility = Visibility.Visible; Grid_Blue_Selection.Visibility = Visibility.Hidden; Grid_Brown_Selection.Visibility = Visibility.Hidden;

        }

        private void Btn_RedSwitch_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colors[2]);

            Lbl_DscrptHeader.Content = "Brown Switch";
            Lbl_DscrptHeader.Foreground = new SolidColorBrush(color);

            Btn_BrownSwitch.Visibility = Visibility.Visible;
            Btn_RedSwitch.Visibility = Visibility.Hidden;

            Txb_Descrition.Text = stringText.getBrownDscrpt();

            // N: Grid Brown Visibility True
            Grid_Brown_Selection.Visibility = Visibility.Visible; Grid_Red_Selection.Visibility = Visibility.Hidden; Grid_Blue_Selection.Visibility = Visibility.Hidden;

        }

        private void Btn_BrownSwitch_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colors[3]);

            Lbl_DscrptHeader.Content = "Welcome To JC.Click";
            Lbl_DscrptHeader.Foreground = new SolidColorBrush(color);

            Btn_DefaultSwitch.Visibility = Visibility.Visible;
            Btn_BrownSwitch.Visibility = Visibility.Hidden;

            Txb_Descrition.Text = stringText.getdefaultScrpt();

            // N: Grid Brown Visibility True
            Grid_Brown_Selection.Visibility = Visibility.Hidden; Grid_Red_Selection.Visibility = Visibility.Hidden; Grid_Blue_Selection.Visibility = Visibility.Hidden;

            backToDefaultSwitch();
        }

        private void Btn_SelectedBlue_Click(object sender, RoutedEventArgs e)
        {
            changeImageBrush.ImageSource = new BitmapImage(new Uri(pathImages.getBlueButton()));
            changeImageBrush.Stretch = Stretch.Uniform;
            Btn_SelectedBlue.Background = changeImageBrush;
        }

        private void Btn_SelectedRed_Click(object sender, RoutedEventArgs e)
        {
            changeImageBrush.ImageSource = new BitmapImage(new Uri(pathImages.getRedButton()));
            changeImageBrush.Stretch = Stretch.Uniform;
            Btn_SelectedRed.Background = changeImageBrush;
        }

        private void Btn_SelectedBrown_Click(object sender, RoutedEventArgs e)
        {
            changeImageBrush.ImageSource = new BitmapImage(new Uri(pathImages.getBrownButton()));
            changeImageBrush.Stretch = Stretch.Uniform;
            Btn_SelectedBrown.Background = changeImageBrush;
        }

        private void backToDefaultSwitch()
        {
            ImageBrush default1 = new ImageBrush();
            ImageBrush default2 = new ImageBrush();
            ImageBrush default3 = new ImageBrush();

            default1.ImageSource = new BitmapImage(new Uri(pathImages.getDefaultButton()));
            default1.Stretch = Stretch.Uniform;
            default2.ImageSource = new BitmapImage(new Uri(pathImages.getDefaultButton()));
            default2.Stretch = Stretch.Uniform;
            default3.ImageSource = new BitmapImage(new Uri(pathImages.getDefaultButton()));
            default3.Stretch = Stretch.Uniform;

            Btn_SelectedBlue.Background = default1;
            Btn_SelectedRed.Background = default2;
            Btn_SelectedBrown.Background = default3;
        }

        private void KeyboardHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            keyboardHook.Unhook();
        }

    }
}