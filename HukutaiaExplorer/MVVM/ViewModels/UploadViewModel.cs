using HukutaiaExplorer.MVVM.Models;
using HukutaiaExplorer.MVVM.Services;
using Firebase.Auth;
using PropertyChanged;
using System;
using CommunityToolkit.Mvvm.Input;

namespace HukutaiaExplorer.MVVM.ViewModels
{
    // Represents the view model for the upload view
    [AddINotifyPropertyChangedInterface]
    public class UploadViewModel
    {
        #region Models & Commands
        // Model for user upload data
        public UploadModel Upload { get; set; }

        // Model for image upload data
        public ImageModel Image { get; set; }

        // Command for create action
        public Command CreateCommand { get; }

        // Command for Pick image action
        public Command PickImageCommand { get; }
        #endregion

        #region View Model Constructor
        // Constructor
        public UploadViewModel()
        {
            // Initialize the login model
            Upload = new UploadModel();
            // Initialize the image model
            Image = new ImageModel();

            // Create an instance of upload service for handling plant data and image data
            var serv = new UploadService(Upload, Image);

            // Assign command to execute the upload service for creating a plant entry
            CreateCommand = new Command(async () => await serv.CreatePlantAsync());
            // Assign command to execute the upload service for picking an image
            PickImageCommand = new Command(async () => await serv.PickImageAsync());
        }
        #endregion
    }
}
