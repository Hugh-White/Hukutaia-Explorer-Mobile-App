using PropertyChanged;
using System;
using Firebase.Auth;
using HukutaiaExplorer.MVVM.Models;
using HukutaiaExplorer.MVVM.Services;

namespace HukutaiaExplorer.MVVM.ViewModels
{
    // Represents the view model for the login view
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel
    {
        // Model for user login data
        public LoginModel Users { get; set; }

        // Command for login action
        public Command LoginService { get; }

        // Constructor
        public LoginViewModel()
        {
            // Initialize the login model
            Users = new LoginModel();

            // Create an instance of login service
            var loginService = new LoginService(Users);

            // Assign command to execute login service
            LoginService = new Command(async () => await loginService.Execute());
        }
    }
}
