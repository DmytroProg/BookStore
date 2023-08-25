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
    /// Interaction logic for AdminMainView.xaml
    /// </summary>
    public partial class AdminMainView : UserControl
    {
        private string savedText = "Saved";
        private string changedText = "*Unsaved";

        public AdminMainView()
        {
            InitializeComponent();
        }

        private void booksDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            changesTextBox.Text = changedText;
            changesTextBox.Foreground = Brushes.Red;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            changesTextBox.Text = savedText;
            changesTextBox.Foreground = Brushes.Green;
        }
    }
}
