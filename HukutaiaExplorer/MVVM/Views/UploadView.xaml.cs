using HukutaiaExplorer.MVVM.ViewModels;

namespace HukutaiaExplorer.MVVM.Views;

public partial class UploadView : ContentPage
{
    // Sets binding context for upload page
    public UploadView()
	{
		InitializeComponent();

        // Create instance of UploadViewModel and set as BindingContext
        UploadViewModel viewModel = new UploadViewModel();
        BindingContext = viewModel;
    }
}