namespace HukutaiaExplorer.MVVM.Models
{
    // Represents the model for user plant data
    public class UploadModel
    {
        // Properties to hold user's plant details
        public string? Name { get; set; }
        public string? Authority { get; set; }
        public string? Family { get; set; }
        public string? Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
