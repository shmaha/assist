using HMS.DataAccess;
using HMS.Entity;
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

namespace HMS.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private ConsultantDataAccess _consultantDA;
        public Settings()
        {
            InitializeComponent();
            _consultantDA = new ConsultantDataAccess();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var consultant = new Consultant();
            consultant.Name = TxtName.Text;
            consultant.MCI = TxtMCI.Text;
            consultant.Fee = Convert.ToInt64(TxtFee.Text);
            consultant.Default = (ChkDefault.IsChecked == true) ? 1 : 0;
            var returned = _consultantDA.Add(consultant);
            if (returned == null)
            {
                LblError.Content = "The record exists already!";
            }
            else
            {
                LblError.Content = "The consultant details were added successfully";
            }
        }
    }
}
