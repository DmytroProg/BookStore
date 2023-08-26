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
using System.Windows.Shapes;

namespace BookStoreCore.Views
{
    /// <summary>
    /// Interaction logic for AddDiscountView.xaml
    /// </summary>
    public partial class AddDiscountView : UserControl
    {
        public AddDiscountView()
        {
            InitializeComponent();
        }

        private void Grid_Checked(object sender, RoutedEventArgs e)
        {
            if (discountDataGrid is null) return;
            if(discountRBtn.IsChecked == true)
            {
                booksDataGrid.Visibility = Visibility.Hidden;
                discountDataGrid.Visibility = Visibility.Visible;
            }
            else
            {
                booksDataGrid.Visibility = Visibility.Visible;
                discountDataGrid.Visibility = Visibility.Hidden;
            }
        }
    }
}
