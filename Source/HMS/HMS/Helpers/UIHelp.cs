using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HMS.Helpers
{
    public class UIHelp
    {
        public void DisplayUIElementInColoumn(Grid parent, UIElement element, int v)
        {
            parent.Children.Add(element);
            Grid.SetRow(element, parent.RowDefinitions.Count() - 1);
            Grid.SetColumn(element, v);
        }
    }
}
