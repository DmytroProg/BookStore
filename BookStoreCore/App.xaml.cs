using BookStoreCore.Services;
using BookStoreCore.Stores;
using BookStoreCore.ViewModels;
using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookStoreCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    /*
    TODO
    - Custom password box
    - Add author popup
     */

    public partial class App : Application
    {
        private NavigationStore _navigationStore = null!;

        public App()
        {
            this._navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrectViewModel = CreateUserLoginViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(this._navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private AdminMainViewModel CreateAdminMainViewModel()
        {
            return new AdminMainViewModel(this._navigationStore);
        }

        private UserLoginViewModel CreateUserLoginViewModel()
        {
            return new UserLoginViewModel(new NavigationService(this._navigationStore, CreateAdminMainViewModel),
                new NavigationService(this._navigationStore, CreateUserMainViewModel));
        }

        private UserMainViewModel CreateUserMainViewModel()
        {
            return new UserMainViewModel(this._navigationStore);
        }
    }
}
