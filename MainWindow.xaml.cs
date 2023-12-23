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
        // N: Array of color codes used in the application.
        String[] colors = { "#0000ff", "#ff0000", "#964B00", "#ffffff" };

        // N: Instance of StringTexts for managing string resources.
        StringTexts stringText = new StringTexts();
        // N: Instance of GetImages for managing image paths.
        GetImages pathImages = new GetImages();
        // N: Instance of ImageBrush for changing button backgrounds.
        ImageBrush changeImageBrush = new ImageBrush();
        // N: Instance of SoundEffects for managing keyboard sound effects.
        SoundEffects sfxKeyboard = new SoundEffects();

        // N: Instance of KeyboardHook for handling keyboard events.
        KeyboardHook keyboardHook;

        // N: Constructor for the main window.
        public MainWindow()
        {
            InitializeComponent();

            // N: Set the default script in the text box.
            Txb_Descrition.Text = stringText.getdefaultScrpt();

            // N: Initialize the keyboard hook and subscribe to the KeyPressed event.
            keyboardHook = new KeyboardHook();
            keyboardHook.KeyPressed += KeyboardHook_KeyPressed;

            // N: Subscribe to the Loaded event.
            Loaded += MainWindow_Loaded;
        }

        // N: Event handler for the Loaded event.
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // N: Subscribe to the TrayMouseDoubleClick event of the system tray icon.
            MyNotifyIcon.TrayMouseDoubleClick += MyNotifyIcon_TrayMouseDoubleClick;
        }

        // N: Event handler for the TrayMouseDoubleClick event of the system tray icon.
        private void MyNotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            // N: Handle double-click on the tray icon (show/hide the window, for example).
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

        // N: Event handler for the Close button click event.
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            // N: Shutdown the application.
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

            // N: Stack Volume Visible
            Stck_Volumes.Visibility = Visibility.Visible;
            
            // N: Button Selected Blue
            Btn_SelectedBlue.Visibility = Visibility.Visible; Btn_SelectedRed.Visibility= Visibility.Hidden; Btn_SelectedBrown.Visibility = Visibility.Hidden;
        }

        private void Btn_BlueSwitch_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colors[1]);

            Lbl_DscrptHeader.Content = "Red Switch";
            Lbl_DscrptHeader.Foreground = new SolidColorBrush(color);

            Btn_RedSwitch.Visibility = Visibility.Visible;
            Btn_BlueSwitch.Visibility = Visibility.Hidden;

            Txb_Descrition.Text = stringText.getRedDscrpt();

            // N: Button Selected Red
            Btn_SelectedBlue.Visibility = Visibility.Hidden; Btn_SelectedRed.Visibility = Visibility.Visible; Btn_SelectedBrown.Visibility = Visibility.Hidden;
        }

        private void Btn_RedSwitch_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colors[2]);

            Lbl_DscrptHeader.Content = "Brown Switch";
            Lbl_DscrptHeader.Foreground = new SolidColorBrush(color);

            Btn_BrownSwitch.Visibility = Visibility.Visible;
            Btn_RedSwitch.Visibility = Visibility.Hidden;

            Txb_Descrition.Text = stringText.getBrownDscrpt();

            // N: Button Selected Brown
            Btn_SelectedBlue.Visibility = Visibility.Hidden; Btn_SelectedRed.Visibility = Visibility.Hidden; Btn_SelectedBrown.Visibility = Visibility.Visible;
        }

        private void Btn_BrownSwitch_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(colors[3]);

            Lbl_DscrptHeader.Content = "Welcome To JC.Click";
            Lbl_DscrptHeader.Foreground = new SolidColorBrush(color);

            Btn_DefaultSwitch.Visibility = Visibility.Visible;
            Btn_BrownSwitch.Visibility = Visibility.Hidden;

            Txb_Descrition.Text = stringText.getdefaultScrpt();

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

            // N: Stack Volume Hidden
            Stck_Volumes.Visibility = Visibility.Hidden;

            // N: Buttons back to Default
            Btn_SelectedBlue.Visibility = Visibility.Hidden; Btn_SelectedRed.Visibility = Visibility.Hidden; Btn_SelectedBrown.Visibility = Visibility.Hidden;
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