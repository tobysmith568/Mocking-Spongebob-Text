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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gma.System.MouseKeyHook;
using Forms = System.Windows.Forms;
using Clipboard = System.Windows.Forms.Clipboard;
using Settings = MockingSpongebobText.Properties.Settings;

namespace MockingSpongebobText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  Variables
        //  =========

        private readonly IKeyboardMouseEvents keyboardMouseEvents;
        private MockType selectedMockType;
        private bool closeLock;
        private bool usingControl;
        private bool usingShift;
        private bool usingAlt;

        //  Properties
        //  ==========

        public Command IconLeftClick { get; set; }
        public static bool UpperCaseLs { get; private set; }
        public static bool LowerCaseIs { get; private set; }
        public static bool LowerCaseOs { get; private set; }

        //  Constructors
        //  ============

        /// <exception cref="System.Runtime.InteropServices.ExternalException"></exception>
        /// <exception cref="System.Threading.ThreadStateException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public MainWindow()
        {
            closeLock = true;

            InitializeComponent();

            foreach (char letter in "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890")
            {
                cbKeys.Items.Add(new ComboBoxItem()
                {
                    Content = letter
                });
            }

            foreach (MockType mockType in Enum.GetValues(typeof(MockType)))
            {
                cbCase.Items.Add(new ComboBoxItem()
                {
                    Content = mockType.ReadableText(),
                    Tag = mockType
                });
            }

            IconLeftClick = new Command(DoIconLeftClick);

            keyboardMouseEvents = Hook.GlobalEvents();
            keyboardMouseEvents.KeyDown += KeyboardMouseEvents_KeyDown;

            ImportSavedData();
        }

        //  Events
        //  ======

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (closeLock)
            {
                Visibility = Visibility.Hidden;
                e.Cancel = true;
            }
        }

        /// <exception cref="System.Runtime.InteropServices.ExternalException"></exception>
        /// <exception cref="System.Threading.ThreadStateException"></exception>
        private void KeyboardMouseEvents_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((usingControl && e.Control == false) ||
               (!usingControl && e.Control == true))
            {
                return;
            }

            if ((usingShift && e.Shift == false) ||
               (!usingShift && e.Shift == true))
            {
                return;
            }

            if ((usingAlt && e.Alt == false) ||
               (!usingAlt && e.Alt == true))
            {
                return;
            }

            if (((char)e.KeyValue) == cbKeys.SelectedItem.ToString()[0])
            {
                if (Clipboard.ContainsText())
                {
                    string content = Clipboard.GetText();

                    content = selectedMockType.MockifyText(content);

                    Clipboard.SetText(content);
                }
            }
        }

        private void CbKeys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.KeyIndex = cbKeys.SelectedIndex;
        }

        /// <exception cref="InvalidOperationException">Ignore.</exception>
        private void CbCase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(cbCase.SelectedItem is ComboBoxItem comboBoxItem))
            {
                throw new InvalidOperationException("Selected item is not a comboBoxItem!");
            }

            if (!(comboBoxItem.Tag is MockType mockType))
            {
                throw new InvalidOperationException("Unknown Case Pattern!");
            }

            selectedMockType = mockType;
            Settings.Default.MockType = (int)mockType;
        }

        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            closeLock = false;
            Close();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UppercaseLs_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.AlwaysUppercaseLs = UpperCaseLs = cbUpperCaseLs.IsChecked == true;
        }

        private void LowerCaseIs_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.AlwaysLowercaseIs = LowerCaseIs = cbLowerCaseIs.IsChecked == true;
        }

        private void LowerCaseOs_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.AlwaysLowerCaseOs = LowerCaseOs = cbLowerCaseOs.IsChecked == true;
        }

        private void UsingControl_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.UsingControl = usingControl = cbUsingControl.IsChecked == true;
        }

        private void UsingShift_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.UsingShift = usingShift = cbUsingShift.IsChecked == true;
        }

        private void UsingAlt_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.UsingAlt = usingAlt = cbUsingAlt.IsChecked == true;
        }

        //  Methods
        //  =======

        private void ImportSavedData()
        {
            cbUsingControl.IsChecked = Settings.Default.UsingControl;
            cbUsingShift.IsChecked = Settings.Default.UsingShift;
            cbUsingAlt.IsChecked = Settings.Default.UsingAlt;

            cbKeys.SelectedIndex = Settings.Default.KeyIndex;

            cbCase.SelectedIndex = Settings.Default.MockType;

            cbUpperCaseLs.IsChecked = Settings.Default.AlwaysUppercaseLs;
            cbLowerCaseIs.IsChecked = Settings.Default.AlwaysLowercaseIs;
            cbLowerCaseOs.IsChecked = Settings.Default.AlwaysLowerCaseOs;
        }

        private void DoIconLeftClick()
        {
            Visibility = Visibility.Visible;
        }
    }
}
