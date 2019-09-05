using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gma.System.MouseKeyHook;

namespace MockingSpongebobText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  Variables
        //  =========

        private IKeyboardMouseEvents keyboardMouseEvents;

        //  Properties
        //  ==========

        public Command IconLeftClick { get; set; }

        //  Constructors
        //  ============

        public MainWindow()
        {
            InitializeComponent();

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

        private void KeyboardMouseEvents_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Shift && e.Alt && e.KeyCode == Keys.S)
            {

            }
        }

        //  Methods
        //  =======

        private void DoIconLeftClick()
        {
            Visibility = Visibility.Visible;
        }
    }
}
