using System;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Storage;
using HukutaiaExplorer.MVVM.Models;
using Newtonsoft.Json;

namespace HukutaiaExplorer.MVVM.Services
{
    // Service responsible for handling upload functionality
    public class UploadService
    {
        #region Properties/Models
        // Property to hold the upload model
        public UploadModel UploadModel;

        // Property to hold the image model
        public ImageModel ImageModel; 
        #endregion

        #region Private Properties
        // Private Properties
        private FirebaseClient firebaseClient;
        private FirebaseStorage firebaseStorage;
        #endregion

        #region Constructor
        // Constructor for handling plant data and image upload data 
        public UploadService(UploadModel uploadModel, ImageModel imageModel)
        {
            // Initialize UploadModel property
            UploadModel = uploadModel;
            // Initialize ImageModel property
            ImageModel = imageModel;

            // Initialize Firebase client for database interaction 
            firebaseClient = new FirebaseClient("https://hukutaia-domain-default-rtdb.asia-southeast1.firebasedatabase.app/");
            // Initialize Firebase storage for uplaoding images
            firebaseStorage = new FirebaseStorage("hukutaia-domain.appspot.com");
        }
        #endregion

        #region Tasks
        // Method to create a new plant entry
        public async Task CreatePlantAsync()
        {
            try
            {
                // Serialize UploadModel to JSON
                string json = JsonConvert.SerializeObject(UploadModel);
                // Post JSON data to Firebase database
                await firebaseClient.Child("Plant").PostAsync(json);
                // Construct storage path for the image
                var firebaseStoragePath = $"images/{ImageModel.FileName}";
                // Upload image to firebase storage
                await firebaseStorage
                    .Child(firebaseStoragePath)
                    .PutAsync(ImageModel.Stream);
                // Display success message
                await Application.Current.MainPage.DisplayAlert("Success", "Plant entry uploaded!", "Ok");

            }
            catch (Exception ex)
            {
                // Display error message in case of failure
                Console.WriteLine($"Error creating plant: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Error uploading plant, please try again later", "Ok");
            }
        }

        // Method to pick an image from the device storage
        public async Task PickImageAsync()
        {
            try
            {
                // Open file picker to select an image
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Select an Image"
                });

                if (result != null)
                {
                    // Set image stream and file name
                    ImageModel.Stream = await result.OpenReadAsync();
                    ImageModel.FileName = result.FileName;
                    // Sets picture string 
                    UploadModel.Picture = ImageModel.FileName;
                    // Display success message
                    await Application.Current.MainPage.DisplayAlert("Image Selected!", $"Image: {result.FileName}", "Ok");
                }
            }
            catch (Exception ex)
            {
                // Display error message in case of failure
                Console.WriteLine($"Error selecting image: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Error selecting image, please try again later", "Ok");
            }
        }
        #endregion
    }
}
