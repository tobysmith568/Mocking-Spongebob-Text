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
            InitializeComponent();

            foreach (char letter in "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890")
            {
                KeyComboBox.Items.Add(new ComboBoxItem()
                {
                    Content = letter
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
            Visibility = Visibility.Hidden;
            e.Cancel = true;
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

        //  Methods
        //  =======

        private void DoIconLeftClick()
        {
            Visibility = Visibility.Visible;
        }

        private string MockifyText(string input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            string result = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                result += i % 2 == 0 ? char.ToUpper(input[i]) : char.ToLower(input[i]);
            }

            return result;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton radioButton))
            {
                throw new ArgumentException("Arg [sender] must be a RadioButton");
            }

            if (!(radioButton.CommandParameter is MockType mockType))
            {
                throw new ArgumentException("Arg [sender] .CommandParameter must be a MockType");
            }

            selectedMockType = mockType;
        }
    }
}
