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

        //  Properties
        //  ==========

        public Command IconLeftClick { get; set; }

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
            if (e.Shift && e.Alt && e.KeyCode == Forms.Keys.S)
            {
                if (Clipboard.ContainsText())
                {
                    string content = Clipboard.GetText();

                    content = selectedMockType.MockifyText(content);

                    Clipboard.SetText(content);
                }
            }
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

        //  Methods
        //  =======

        private void DoIconLeftClick()
        {
            Visibility = Visibility.Visible;
        }
    }
}
