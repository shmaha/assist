using HMS.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace HMS.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            using (var db = new HMSEntities())
            {
                var list = db.Consultants.ToList();
                this.BtnSettings.Content = list.First().Name;
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Uri navUri = new Uri(@"Pages\RegisterPatient.xaml", UriKind.Relative);
            Trace.WriteLine("Navigating to RegisterPatient");
            this.NavigationService.Navigate(navUri);
        }

        private void BtnPrescription_Click(object sender, RoutedEventArgs e)
        {
            Uri navUri = new Uri(@"Pages\WritePrescription.xaml", UriKind.Relative);
            Trace.WriteLine("Navigating to WritePrescription");
            this.NavigationService.Navigate(navUri);
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Uri navUri = new Uri(@"Pages\Settings.xaml", UriKind.Relative);
            Trace.WriteLine("Navigating to Settings");
            this.NavigationService.Navigate(navUri);
        }
    }
}
