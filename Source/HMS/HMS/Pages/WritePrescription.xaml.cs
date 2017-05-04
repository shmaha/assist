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

using HMS.Model;
using HMS.Enums;

using System.Diagnostics;

namespace HMS.Pages
{
    /// <summary>
    /// Interaction logic for WritePrescription.xaml
    /// </summary>
    public partial class WritePrescription : Page
    {
        public List<Drug> test;
        public List<string> display;
        #region variables
        public ComboBox drugForm;
        public ComboBox drugRoute;
        public ComboBox drugFrequency;
        public TextBox drugName;
        public TextBox drugBrand;
        public TextBox drugGeneric;
        public Button btnDone;
        #endregion
        public WritePrescription()
        {
            InitializeComponent();
            display = new List<string>();
            #region Temp
            List<string> drugName = new List<string>();
            
            test = new List<Drug>();
            test.Add(new Drug() { DrugName = "Freeze DSR", DrugForm = "Cap", Brand = "One Pharma", Route = "Orally", Frequency = "OD", GenericName = "PANTAPRAZOLE 30MG SUSTAINED RELEASE" });
            test.Add(new Drug() { DrugName = "Comfy", DrugForm = "Tab", Brand = "One Pharma", Route = "Orally", Frequency = "BD", GenericName = "ACECLOFENAC 100MG + PARACETAMOL 350 MG" });
            test.Add(new Drug() { DrugName = "comfy2", DrugForm = "Tab", Brand = "One Pharma", Route = "Orally", Frequency = "BD", GenericName = "ACECLOFENAC 100MG + PARACETAMOL 350 MG" });
            test.Add(new Drug() { DrugName = "Comfy3", DrugForm = "Tab", Brand = "One Pharma", Route = "Orally", Frequency = "BD", GenericName = "ACECLOFENAC 100MG + PARACETAMOL 350 MG" });
            #endregion
            //test = new DataRepository<Drug>().GetAll().ToList();

            foreach (var t in test)
            {
                drugName.Add(t.DrugName);
            }
            //this.ComboList.ItemsSource = drugName;
        }
                
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = TxtSearch.Text;
            List<string> autoSuggest = new List<string>();
            autoSuggest.Clear();

            foreach (var item in test)
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
                        obj = test.Where(x => x.DrugName == TxtSearch.Text).FirstOrDefault();
                    }
                    else
                    {
                        obj = test.Where(x => x.GenericName == TxtSearch.Text).FirstOrDefault();
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
            #endregion

            #region Display elements
            this.TbPrescription.Children.Add(drugForm);
            Grid.SetRow(drugForm, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugForm, 0);

            this.TbPrescription.Children.Add(drugName);
            Grid.SetRow(drugName, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugName, 1);

            this.TbPrescription.Children.Add(drugBrand);
            Grid.SetRow(drugBrand, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugBrand, 2);

            this.TbPrescription.Children.Add(drugGeneric);
            Grid.SetRow(drugGeneric, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugGeneric, 3);

            this.TbPrescription.Children.Add(drugRoute);
            Grid.SetRow(drugRoute, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugRoute, 4);

            this.TbPrescription.Children.Add(drugFrequency);
            Grid.SetRow(drugFrequency, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugFrequency, 5);
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

            string displayCurrent = drugForm.Text +". " + drugName.Text +" ("+drugBrand.Text+") "+drugRoute.Text+" "+drugFrequency.Text+ ";(" + drugGeneric.Text+") " + displayFreq;
            display.Add(displayCurrent);

            TxtSearch.Text = string.Empty;
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

            btnDone = new Button();
            btnDone.Content = "Done";
            btnDone.Click += BtnDone_Click;
            btnDone.HorizontalAlignment = HorizontalAlignment.Center;
            
            #endregion

            #region Display elements
            this.TbPrescription.Children.Add(drugForm);
            Grid.SetRow(drugForm, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugForm, 0);

            this.TbPrescription.Children.Add(drugName);
            Grid.SetRow(drugName, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugName, 1);

            this.TbPrescription.Children.Add(drugBrand);
            Grid.SetRow(drugBrand, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugBrand, 2);

            this.TbPrescription.Children.Add(drugGeneric);
            Grid.SetRow(drugGeneric, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugGeneric, 3);

            this.TbPrescription.Children.Add(drugRoute);
            Grid.SetRow(drugRoute, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugRoute, 4);

            this.TbPrescription.Children.Add(drugFrequency);
            Grid.SetRow(drugFrequency, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(drugFrequency, 5);

            this.TbPrescription.Children.Add(btnDone);
            Grid.SetRow(btnDone, TbPrescription.RowDefinitions.Count() - 1);
            Grid.SetColumn(btnDone, 6);
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

            // Add the object to db

            test.Add(drug);

            // Replace values that are visible
            TxtSearch.Text = drug.DrugName;
            this.TbPrescription.RowDefinitions.RemoveAt(row);
            this.drugName.Visibility = Visibility.Hidden;
            this.drugForm.Visibility = Visibility.Hidden;
            this.drugRoute.Visibility = Visibility.Hidden; 
            this.drugFrequency.Visibility = Visibility.Hidden; 
            this.drugBrand.Visibility = Visibility.Hidden; 
            this.drugGeneric.Visibility = Visibility.Hidden;
            this.btnDone.Visibility = Visibility.Hidden;

            DisplayUI(test.Where(x => x.DrugName == TxtSearch.Text).FirstOrDefault());
    }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            //Uri navUri = new Uri(@"Pages\PrintLayout.xaml", UriKind.Relative);
            Trace.WriteLine("Navigating to PrintLayout");
            this.NavigationService.Navigate(new PrintLayout(display));
        }
    }
}
