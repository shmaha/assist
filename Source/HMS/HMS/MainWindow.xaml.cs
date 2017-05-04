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

namespace HMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Navigating to Register a patient");
            //NavigationService.GetNavigationService(new RegisterPatient()).Navigate(new Uri("RegisterPatient.xaml", UriKind.Relative));
            //Uri navUri = new Uri("Pages/RegisterPatient.xaml", UriKind.Relative);
            //_NavDock.NavigationService.Navigate(navUri);
        }

        private void BtnPrescription_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
