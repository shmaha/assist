using HMS.DataAccess;
using HMS.Entity;
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
    /// Interaction logic for RegisterPatient.xaml
    /// </summary>
    public partial class RegisterPatient : Page
    {
        private ConsultantDataAccess _consultantDA;
        private PatientDataAccess _patientDA;

        public RegisterPatient()
        {
            InitializeComponent();
            _consultantDA = new ConsultantDataAccess();
            _patientDA = new PatientDataAccess();
            var list = _consultantDA.GetAll().ToList();
            //foreach (Consultant i in TxtConsultant.ItemsSource)
            //{
            //    TxtConsultant.DisplayMemberPath = i.Name;
            //}
            Binding bind = new Binding();
            bind.Source = list;

            TxtConsultant.SetBinding(ItemsControl.ItemsSourceProperty, bind);
            TxtConsultant.DisplayMemberPath = "Name";
            TxtConsultant.SelectedValuePath = "MCI";
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Refresh();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient();
            bool flag = false;
            // Validate data
            string gender;
            if (RadioFemale.IsChecked.Equals(true))
            {
                gender = "Female";
            }
            else if (RadioMale.IsChecked.Equals(true))
            {
                gender = "Male";
            }
            else
            {
                gender = "Other";
            }

            // Save data to db
            
            patient.Name = TxtName.Text != null ? TxtName.Text : null;
            patient.Age = Convert.ToInt64(TxtAge.Text);
            patient.DoB = TxtDob.Text;
            patient.Address = TxtAddress.Text;
            Consultant selected = TxtConsultant.SelectedItem as Consultant;
            patient.Consultant = selected.MCI;
            patient.Village = TxtVillage.Text;
            patient.Phone = TxtPhone.Text != null ? TxtPhone.Text : null;
            patient.Gender = gender;
            if (patient.Name == null || patient.Phone == null)
            {
                LblError.Content = "The name and phone fields are mandatory";
                flag = true;
            }
            if (!flag)
            {
                flag = _patientDA.Add(patient);

                // Navigate to prescription
                if (flag)
                {
                    Trace.WriteLine("Navigating to WritePrescription");
                    this.NavigationService.Navigate(new WritePrescription(patient));
                }
                else
                {
                    LblError.Content = "The user already exists";
                }
            }
        }

        public void TxtDob_SelectedDateChanged(object sender, EventArgs e)
        {
            int age = DateTime.UtcNow.Year - TxtDob.SelectedDate.Value.Year;
            TxtAge.Text = age.ToString();
        }

        private void TxtConsultant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as Consultant;
            TxtCharges.Text = _consultantDA.GetByMci(item.MCI).Fee.ToString();
        }
    }
}
