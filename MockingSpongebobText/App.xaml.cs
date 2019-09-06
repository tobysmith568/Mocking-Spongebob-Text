using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MockingSpongebobText
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <exception cref="System.Runtime.InteropServices.ExternalException">Ignore.</exception>
        /// <exception cref="System.Threading.ThreadStateException">Ignore.</exception>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow window = new MainWindow();

            if (e.Args.Length > 0 && e.Args[0].ToLower() == "nogui")
            {
                return;
            }

            window.Show();
        }
    }
}
