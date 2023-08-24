using BusinessLogicLayer.Models;
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
    /// Interaction logic for AddBookView.xaml
    /// </summary>
    public partial class AddBookView : UserControl
    {
        public AddBookView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            genrePopup.IsOpen = true;
        }

        private void SetGenres()
        {
            genresWrapPanel.Children.Clear();
            foreach (Genre item in genreList.Items)
            {
                if (item.IsSelected)
                {
                    var color = Colors.Gray;
                    color.A = 125;

                    genresWrapPanel.Children.Add(new Border()
                    {
                        CornerRadius = new CornerRadius(12),
                        Background = new SolidColorBrush(color),
                        Margin = new Thickness(0, 0, 10, 0),
                        Child = new TextBlock()
                        {
                            Text = item.GenreName,
                            FontSize = 18,
                            Padding = new Thickness(5)
                        }
                    });
                }
            }
            }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetGenres();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetGenres();
        }

        private void addAuthorBtn_Click(object sender, RoutedEventArgs e)
        {
            authorPanel.IsOpen = true;
        }
    }
}
