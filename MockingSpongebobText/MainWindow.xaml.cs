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

namespace MockingSpongebobText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  Properties
        //  ==========

        public Command IconLeftClick { get; set; }

        //  Constructors
        //  ============

        public MainWindow()
        {
            InitializeComponent();

            IconLeftClick = new Command(DoIconLeftClick);

        }

        //  Events
        //  ======

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        //  Methods
        //  =======

        private void DoIconLeftClick()
        {
            Visibility = Visibility.Visible;
        }
    }
}
