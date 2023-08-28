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
    /// Interaction logic for UserMainView.xaml
    /// </summary>
    public partial class UserMainView : UserControl
    {
        public UserMainView()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            savedBooksPopup.IsOpen = true;
        }

        private void myBooksTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            boughtBooksPopup.IsOpen = true;
        }
    }
}
