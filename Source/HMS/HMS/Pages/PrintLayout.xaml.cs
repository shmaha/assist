using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for PrintLayout.xaml
    /// </summary>
    public partial class PrintLayout : Page
    {
        public PrintLayout(List<string> drugs)
        {
            InitializeComponent();
            foreach (var drug in drugs)
            {
                this.PrintGrid.RowDefinitions.Add(new RowDefinition());
                string[] text = drug.Split(new char[] { ';' },2,StringSplitOptions.RemoveEmptyEntries);
                TextBlock textDrug = new TextBlock();
                textDrug.Text = text[0];
                textDrug.HorizontalAlignment = HorizontalAlignment.Left;
                textDrug.VerticalAlignment = VerticalAlignment.Center;
                //textDrug.Background = Brushes.DarkKhaki;
                textDrug.TextWrapping = TextWrapping.Wrap;
                textDrug.FontSize = 14;
                textDrug.Margin = new Thickness(0, 0, 0, 2);

                this.PrintGrid.Children.Add(textDrug);
                Grid.SetRow(textDrug, this.PrintGrid.RowDefinitions.Count() - 1);
                Grid.SetColumn(textDrug, 0);

                this.PrintGrid.RowDefinitions.Add(new RowDefinition());
                TextBlock textGeneric = new TextBlock();
                textGeneric.Text = text[1];
                textGeneric.HorizontalAlignment = HorizontalAlignment.Left;
                textGeneric.VerticalAlignment = VerticalAlignment.Center;
                //textDrug.Background = Brushes.DarkKhaki;
                textGeneric.TextWrapping = TextWrapping.Wrap;
                textGeneric.FontSize = 14;
                textGeneric.Margin = new Thickness(10, 0, 0, 10);

                this.PrintGrid.Children.Add(textGeneric);
                Grid.SetRow(textGeneric, this.PrintGrid.RowDefinitions.Count() - 1);
                Grid.SetColumn(textGeneric, 0);

            }
            
            PrintPage();
        }

        private void PrintPage()
        {
            PrintDialog printDialog = new PrintDialog();
            if ((bool) printDialog.ShowDialog().GetValueOrDefault())
            {
                PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
                printDialog.PrintVisual(this.PrintLayoutPage, "Test");
            }
        }
    }
}
