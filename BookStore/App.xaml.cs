using BookStore.Stores;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private NavigationStore _navigationStore;

        public App()
        {
            this._navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrectViewModel = new AdminMainViewModel(this._navigationStore, CreateBookViewModel);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(this._navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private BookViewModel CreateBookViewModel()
        {
            return new BookViewModel(this._navigationStore, AdminMainViewModel);
        }

        private AdminMainViewModel AdminMainViewModel()
        {
            return new AdminMainViewModel(this._navigationStore, CreateBookViewModel);
        }
    }
}
