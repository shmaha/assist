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

using HMS.Entity;
using HMS.Enums;
using HMS.Helpers;

using System.Diagnostics;
using HMS.DataAccess;

namespace HMS.Pages
{
    /// <summary>
    /// Interaction logic for WritePrescription.xaml
    /// </summary>
    public partial class WritePrescription : Page
    {
        private List<Drug> drugs = new List<Drug>();
        private List<string> display;
        private DrugDataAccess _drugDA;
        private ConsultantDataAccess _consultantDA;
        private Patient patient;
        #region variables
        public ComboBox drugForm;
        public ComboBox drugRoute;
        public ComboBox drugFrequency;
        public TextBox drugName;
        public TextBox drugBrand;
        public TextBox drugGeneric;
        public Button btnDone;
        public TextBox drugDays;
        #endregion
        public WritePrescription(Patient patient)
        {
            InitializeComponent();
            _drugDA = new DrugDataAccess();
            _consultantDA = new ConsultantDataAccess();
            display = new List<string>();
            drugs = _drugDA.GetAll();
            this.patient = patient;
            
            // Set patient data grid if present
            if (this.patient != null)
            {
                PatientDetails.Visibility = Visibility.Visible;
                TxtPatientName.Text = patient.Name;
                TxtPatientAge.Text = patient.Age.ToString();
                TxtPatientContact.Text = patient.Phone;
                TxtPatientGender.Text = patient.Gender;
            }
            else
            {
                PatientDetails.Visibility = Visibility.Collapsed;
                
            }
        }
                
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = TxtSearch.Text;
            List<string> autoSuggest = new List<string>();
            autoSuggest.Clear();

            foreach (var item in drugs)
            {
                if(!string.IsNullOrEmpty(TxtSearch.Text))
                {
                    if (ChkGeneric.IsChecked == true && item.GenericName.ToUpper().Contains(searchText.ToUpper()))
                    {
                        autoSuggest.Add(item.GenericName);
                    }
                    else if (item.DrugName.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
                    {
                        autoSuggest.Add(item.DrugName);
                    }
                }
            }

            if (autoSuggest.Count > 0)
            {
                LblSuggestion.ItemsSource = autoSuggest;
                LblSuggestion.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrEmpty(TxtSearch.Text))
            {
                LblSuggestion.ItemsSource = null;
                LblSuggestion.Visibility = Visibility.Collapsed;
            }
            else
            {
                LblSuggestion.ItemsSource = null;
                LblSuggestion.Visibility = Visibility.Collapsed;
            }
        }

        private void LblSuggestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LblSuggestion.ItemsSource != null)
            {
                Drug obj;
                LblSuggestion.Visibility = Visibility.Collapsed;
                TxtSearch.TextChanged -= new TextChangedEventHandler(TxtSearch_TextChanged);
                if (LblSuggestion.SelectedIndex != -1)
                {
                    TxtSearch.Text = LblSuggestion.SelectedItem.ToString();

                    if (ChkGeneric.IsChecked == false)
                    {
                        // get the object in question
                        obj = drugs.Where(x => x.DrugName == TxtSearch.Text).FirstOrDefault();
                    }
                    else
                    {
                        obj = drugs.Where(x => x.GenericName == TxtSearch.Text).FirstOrDefault();
                    }
                    DisplayUI(obj);
                }
                TxtSearch.TextChanged += new TextChangedEventHandler(TxtSearch_TextChanged);
            }
        }

        private void DisplayUI(Drug obj)
        {
            // Add a new Row Definition
            this.TbPrescription.RowDefinitions.Add(new RowDefinition());

            #region UI elements

            // UI element for DrugName
            TextBlock drugName = new TextBlock();
            drugName.Text = obj.DrugName;
            drugName.HorizontalAlignment = HorizontalAlignment.Center;
            drugName.VerticalAlignment = VerticalAlignment.Center;
            drugName.TextWrapping = TextWrapping.Wrap;

            TextBlock drugForm = new TextBlock();
            drugForm.Text = obj.DrugForm;
            drugForm.HorizontalAlignment = HorizontalAlignment.Center;
            drugForm.VerticalAlignment = VerticalAlignment.Center;
            drugForm.TextWrapping = TextWrapping.Wrap;

            TextBlock drugBrand = new TextBlock();
            drugBrand.Text = obj.Brand;
            drugBrand.HorizontalAlignment = HorizontalAlignment.Center;
            drugBrand.VerticalAlignment = VerticalAlignment.Center;
            drugBrand.TextWrapping = TextWrapping.Wrap;

            TextBlock drugGeneric = new TextBlock();
            drugGeneric.Text = obj.GenericName.ToUpper();
            drugGeneric.HorizontalAlignment = HorizontalAlignment.Center;
            drugGeneric.VerticalAlignment = VerticalAlignment.Center;
            drugGeneric.TextWrapping = TextWrapping.Wrap;

            TextBlock drugRoute = new TextBlock();
            drugRoute.Text = obj.Route;
            drugRoute.HorizontalAlignment = HorizontalAlignment.Center;
            drugRoute.VerticalAlignment = VerticalAlignment.Center;
            drugRoute.TextWrapping = TextWrapping.Wrap;

            TextBox drugFrequency = new TextBox();
            drugFrequency.Text = obj.Frequency;
            drugFrequency.HorizontalAlignment = HorizontalAlignment.Center;
            drugFrequency.VerticalAlignment = VerticalAlignment.Center;
            drugFrequency.TextWrapping = TextWrapping.Wrap;

            TextBox drugDays = new TextBox();
            drugDays.Text = obj.Days.ToString();
            drugDays.HorizontalAlignment = HorizontalAlignment.Center;
            drugDays.VerticalAlignment = VerticalAlignment.Center;
            drugDays.TextWrapping = TextWrapping.Wrap;

            Button btnDelete = new Button();
            btnDelete.Content = "Delete";
            btnDelete.Click += BtnDelete_Click;
            btnDelete.HorizontalAlignment = HorizontalAlignment.Center;

            #endregion

            #region Display elements
            var ui = new UIHelp();
            ui.DisplayUIElementInColoumn(TbPrescription,drugForm, 0);
            //this.TbPrescription.Children.Add(drugForm);
            //Grid.SetRow(drugForm, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugForm, 0);

            ui.DisplayUIElementInColoumn(TbPrescription, drugName, 1);
            //this.TbPrescription.Children.Add(drugName);
            //Grid.SetRow(drugName, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugName, 1);

            ui.DisplayUIElementInColoumn(TbPrescription, drugBrand, 2);
            //this.TbPrescription.Children.Add(drugBrand);
            //Grid.SetRow(drugBrand, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugBrand, 2);

            ui.DisplayUIElementInColoumn(TbPrescription, drugGeneric, 3);
            //this.TbPrescription.Children.Add(drugGeneric);
            //Grid.SetRow(drugGeneric, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugGeneric, 3);

            ui.DisplayUIElementInColoumn(TbPrescription, drugRoute, 4);
            //this.TbPrescription.Children.Add(drugRoute);
            //Grid.SetRow(drugRoute, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugRoute, 4);

            ui.DisplayUIElementInColoumn(TbPrescription, drugFrequency, 5);
            //this.TbPrescription.Children.Add(drugFrequency);
            //Grid.SetRow(drugFrequency, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugFrequency, 5);

            ui.DisplayUIElementInColoumn(TbPrescription, drugDays, 6);
            //this.TbPrescription.Children.Add(drugDays);
            //Grid.SetRow(drugDays, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugDays, 6);

            ui.DisplayUIElementInColoumn(TbPrescription, btnDelete, 7);
            #endregion

            // Add to display string
            string displayFreq = null;
            switch(drugFrequency.Text)
            {
                case "OD": displayFreq = "ONCE A DAY"; break;
                case "BD": displayFreq = "TWICE A DAY"; break;
                case "ES": displayFreq = "EMPTY STOMACH"; break;
                case "HS": displayFreq = "AT BEDTIME"; break;
                default: break;
            }

            string displayCurrent = drugForm.Text +". " + drugName.Text +" ("+drugBrand.Text+") "+drugRoute.Text+" "+drugFrequency.Text+ ";(" + drugGeneric.Text+") " + displayFreq + " for " + drugDays.Text +" days";
            display.Add(displayCurrent);

            TxtSearch.Text = string.Empty;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //int row = Grid.GetRow(e.Source as Button);
            //int coloumn = Grid.GetColumn(e.Source as Button);

            //Drug drug = new Drug();
            //drug.DrugForm = drugForm.SelectedValue.ToString();
            //drug.DrugName = drugName.Text[0].ToString().ToUpper() + drugName.Text.Substring(1);
            //drug.Brand = drugBrand.Text[0].ToString().ToUpper() + drugBrand.Text.Substring(1);
            //drug.GenericName = drugGeneric.Text.ToUpper();
            //drug.Route = drugRoute.SelectedValue.ToString();
            //drug.Frequency = drugFrequency.SelectedValue.ToString();
            //drug.Days = Convert.ToInt32(drugDays.Text);

            //// Add the object to db
            //_drugDA.Delete(drug);
            
            //// Replace values that are visible
            //TxtSearch.Text = drug.DrugName;
            //this.TbPrescription.RowDefinitions.RemoveAt(row);
            //this.drugName.Visibility = Visibility.Hidden;
            //this.drugForm.Visibility = Visibility.Hidden;
            //this.drugRoute.Visibility = Visibility.Hidden;
            //this.drugFrequency.Visibility = Visibility.Hidden;
            //this.drugBrand.Visibility = Visibility.Hidden;
            //this.drugGeneric.Visibility = Visibility.Hidden;
            //this.drugDays.Visibility = Visibility.Hidden;
            //this.btnDone.Visibility = Visibility.Hidden;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.TbPrescription.RowDefinitions.Add(new RowDefinition());
            #region UI elements

            // UI element for DrugName
            drugName = new TextBox();
            drugName.Text = TxtSearch.Text;
            drugName.HorizontalAlignment = HorizontalAlignment.Center;
            drugName.VerticalAlignment = VerticalAlignment.Center;

            drugForm = new ComboBox();
            drugForm.HorizontalAlignment = HorizontalAlignment.Center;
            drugForm.VerticalAlignment = VerticalAlignment.Center;
            drugForm.ItemsSource = Enum.GetValues(typeof(DrugForm));

            drugBrand = new TextBox();
            drugBrand.TextWrapping = TextWrapping.Wrap;
            drugBrand.HorizontalAlignment = HorizontalAlignment.Center;
            drugBrand.VerticalAlignment = VerticalAlignment.Center;

            drugGeneric = new TextBox();
            drugGeneric.TextWrapping = TextWrapping.Wrap;
            drugGeneric.HorizontalAlignment = HorizontalAlignment.Center;
            drugGeneric.VerticalAlignment = VerticalAlignment.Center;

            drugRoute = new ComboBox();
            drugRoute.HorizontalAlignment = HorizontalAlignment.Center;
            drugRoute.VerticalAlignment = VerticalAlignment.Center;
            drugRoute.ItemsSource = Enum.GetValues(typeof(DrugRoute));

            drugFrequency = new ComboBox();
            drugFrequency.HorizontalAlignment = HorizontalAlignment.Center;
            drugFrequency.VerticalAlignment = VerticalAlignment.Center;
            drugFrequency.ItemsSource = Enum.GetValues(typeof(DrugFrequency));

            drugDays = new TextBox();
            drugDays.TextWrapping = TextWrapping.Wrap;
            drugDays.HorizontalAlignment = HorizontalAlignment.Center;
            drugDays.VerticalAlignment = VerticalAlignment.Center;

            btnDone = new Button();
            btnDone.Content = "Done";
            btnDone.Click += BtnDone_Click;
            btnDone.HorizontalAlignment = HorizontalAlignment.Center;

            #endregion

            #region Display elements
            var ui = new UIHelp();

            ui.DisplayUIElementInColoumn(TbPrescription, drugForm, 0);
            //this.TbPrescription.Children.Add(drugForm);
            //Grid.SetRow(drugForm, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugForm, 0);

            ui.DisplayUIElementInColoumn(TbPrescription, drugName, 1);
            //this.TbPrescription.Children.Add(drugName);
            //Grid.SetRow(drugName, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugName, 1);

            ui.DisplayUIElementInColoumn(TbPrescription, drugBrand, 2);
            //this.TbPrescription.Children.Add(drugBrand);
            //Grid.SetRow(drugBrand, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugBrand, 2);

            ui.DisplayUIElementInColoumn(TbPrescription, drugGeneric, 3);
            //this.TbPrescription.Children.Add(drugGeneric);
            //Grid.SetRow(drugGeneric, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugGeneric, 3);

            ui.DisplayUIElementInColoumn(TbPrescription, drugRoute, 4);
            //this.TbPrescription.Children.Add(drugRoute);
            //Grid.SetRow(drugRoute, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugRoute, 4);

            ui.DisplayUIElementInColoumn(TbPrescription, drugFrequency, 5);
            //this.TbPrescription.Children.Add(drugFrequency);
            //Grid.SetRow(drugFrequency, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugFrequency, 5);

            ui.DisplayUIElementInColoumn(TbPrescription, drugDays, 6);
            //this.TbPrescription.Children.Add(drugDays);
            //Grid.SetRow(drugDays, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(drugDays, 6);

            ui.DisplayUIElementInColoumn(TbPrescription, btnDone, 7);
            //this.TbPrescription.Children.Add(btnDone);
            //Grid.SetRow(btnDone, TbPrescription.RowDefinitions.Count() - 1);
            //Grid.SetColumn(btnDone, 7);
            #endregion
        }

        private void BtnDone_Click(object sender, RoutedEventArgs e)
        {
           
            //string drugName = TbPrescription[].Value.ToString();
            int row = Grid.GetRow(e.Source as Button);
            int coloumn = Grid.GetColumn(e.Source as Button);

            Drug drug = new Drug();
            drug.DrugForm = drugForm.SelectedValue.ToString();
            drug.DrugName = drugName.Text[0].ToString().ToUpper() + drugName.Text.Substring(1);
            drug.Brand = drugBrand.Text[0].ToString().ToUpper() + drugBrand.Text.Substring(1);
            drug.GenericName = drugGeneric.Text.ToUpper();
            drug.Route = drugRoute.SelectedValue.ToString();
            drug.Frequency = drugFrequency.SelectedValue.ToString();
            drug.Days = Convert.ToInt32(drugDays.Text);

            // Add the object to db
            _drugDA.Add(drug);
            drugs = _drugDA.GetAll();

            // Replace values that are visible
            TxtSearch.Text = drug.DrugName;
            this.TbPrescription.RowDefinitions.RemoveAt(row);
            this.drugName.Visibility = Visibility.Hidden;
            this.drugForm.Visibility = Visibility.Hidden;
            this.drugRoute.Visibility = Visibility.Hidden; 
            this.drugFrequency.Visibility = Visibility.Hidden; 
            this.drugBrand.Visibility = Visibility.Hidden; 
            this.drugGeneric.Visibility = Visibility.Hidden;
            this.drugDays.Visibility = Visibility.Hidden;
            this.btnDone.Visibility = Visibility.Hidden;

            DisplayUI(drugs.Where(x => x.DrugName == TxtSearch.Text).FirstOrDefault());
    }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            //Uri navUri = new Uri(@"Pages\PrintLayout.xaml", UriKind.Relative);
            Consultant consultant;
            if (this.patient != null)
            {
                consultant = _consultantDA.GetByMci(patient.Consultant);
            }
            else
            {
                consultant = _consultantDA.GetDefault();
            }
            Trace.WriteLine("Navigating to PrintLayout");
            this.NavigationService.Navigate(new PrintLayout(display,consultant));
        }
    }
}
