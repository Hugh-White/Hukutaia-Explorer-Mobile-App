using CsvHelper;
using HukutaiaExplorer.MVVM.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using PropertyChanged;
using System.Globalization;
using System.Reflection;


namespace HukutaiaExplorer.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GuideViewModel
    {
        #region Properties & Fields
        //Properties to store image and blurb details for pins, bound to dialogue box output controls.
        public string? PinImage { get; set; }
        public string? PinDetails { get; set; }
       
        // Dictionary to store pin details for Dialogue Box
        private Dictionary<string, (string ImageFilename, string Description)>? pinDetails;
        //List for map pin objects to check proximity to user
        private List<Pin> Pins { get; set; }
        //Bool variable to toggle tracking feature with page appearing/disappearing
        private bool isTrackingLocation;
        #endregion

        #region Constructor
        //In this paramterless constructor, we set the values of PinImage & PinDetails to have some default properties initialized and ready for use in the dialogue box.
        //We also call the InitializePinDetails method, to add details to Pins for the use of our map.
        public GuideViewModel()
        {
            InitializePinDetails();
            PinImage = "hukflower";
            PinDetails = "Haere Mai! Welcome to the Hukutaia Domain! Keep this page open while you walk to learn about the culture and plantlife in the area! \n\n There are many beautiful and meaningful things for you to discover!";
            Pins = new List<Pin>();
        }
        #endregion

        #region Tracking & Proximity Checking
        // Location Tracking and Proximity Check
        public async Task StartLocationTrackingAsync()
        {
            isTrackingLocation = true;

            while (isTrackingLocation)
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    CheckProximityToPins(location);
                }

                await Task.Delay(30000); // Check every 30 seconds
            }
        }

        private void CheckProximityToPins(Location userLocation)
        {
            foreach (var pin in Pins)
            {
                var pinLocation = new Location(pin.Location.Latitude, pin.Location.Longitude);
                var distance = Location.CalculateDistance(userLocation, pinLocation, DistanceUnits.Kilometers);

                if (distance <= 0.1) // Within 100 meters
                {
                    OnPinClicked(pin, null);
                    break;
                }
            }
        }

        public void StopLocationTracking()
        {
            isTrackingLocation = false;
        }
        #endregion

        #region CSV File Reader (Extracting Lat/Long Values From Excel Spreadsheet)
        // Method to read the CSV file with the path points and save lat/long coords into a list of objects.
        // This method is private as it functions within the view model only.
        private List<GuideModel> ReadLocationsFromCsv()
        {
            var locations = new List<GuideModel>();

            // Get the stream of the CSV file from the raw resources
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("HukutaiaExplorer.Resources.Raw.hukWalk.csv");

            if (stream == null)
            {
                // Handle the case where the stream is null
                throw new FileNotFoundException("The CSV file 'hukWalk.csv' was not found in the resources.");
            }

            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                locations = csv.GetRecords<GuideModel>().ToList();
            }
            return locations;
        }
        #endregion

        #region PolyLine Converter
        //Method to convert a list of lat/long coords into a list of polylines for a map, to draw desired route. Setting map line colours here too.
        //The map being drawn itself will be implemented in another method, where this method will be called and used.
        public IEnumerable<Polyline> ConvertToMapPolylines(List<GuideModel> pathPoints)
        {
            var polylines = new List<Polyline>();

            // Create a polyline from the list of locations
            var polyline = new Polyline
            {
                StrokeColor = Color.FromArgb("FF0000")// Set stroke color to red
            };
            foreach (var location in pathPoints)
            {
                polyline.Geopath.Add(new Location(location.Latitude, location.Longitude));
            }

            polylines.Add(polyline);

            return polylines;
        }
        #endregion

        #region Pin Clicked Event Handler
        //This method is to handle the pin click event. When a pin is clicked, if the dictionary of pinDetails contains the same value as the selected pins label,
        //then the PinImage and PinDetails properties are changed, changing the image and text in the dialogue box.
        public void OnPinClicked(object? sender, PinClickedEventArgs? e)
        {
            var pin = sender as Pin;
            if (pin != null && pinDetails != null && pinDetails.ContainsKey(pin.Label))
            {
                var details = pinDetails[pin.Label];
                PinImage = details.ImageFilename;
                PinDetails = details.Description;
            }

        }
        #endregion

        #region Pin Details (Dialogue box Image/LabelText)
        //This method allows us to set the potential content of the dialogue box beneath the map.
        //Each entry in the dictionary is a potential image source and label text for the dialogue box, which will be changed depending on the pin currently selected on the map.
        //This method is also private, as it isnt used outside of the view model.
        private void InitializePinDetails()
        {
            pinDetails = new Dictionary<string, (string ImageFilename, string Description)>
            {
                { "The Burial Tree", ("burialtree.jpg", "A culturally significant area, where local iwi would process and store the bones of their most prominent figures.") },
                { "The Hukutaia Domain", ("hukdom.jpg", "Welcome to the Hukutaia Domain. Enjoy the nature, get started on the track and take in the beauty of the culture, flora, and fauna!") }
            };
        }
        #endregion

        #region Adding Pins
        //Method to add pins to the map, here we can add or remove any pins we like, then call this in the view code behind and pass it the hukMap to add the pins there.
        public void AddPinsToMap(Microsoft.Maui.Controls.Maps.Map map)
        {
            // Add the existing pins with their event handlers
            var pin1 = new Pin
            {
                Label = "The Burial Tree",
                Location = new Location(-38.06967, 177.26533),
            };
            pin1.MarkerClicked += OnPinClicked;
            map.Pins.Add(pin1);
            Pins.Add(pin1);

            var pin2 = new Pin
            {
                Label = "The Hukutaia Domain",
                Location = new Location(-38.06971, 177.26365),
            };
            pin2.MarkerClicked += OnPinClicked;
            map.Pins.Add(pin2);
            Pins.Add(pin2);
        }
        #endregion

        #region Drawing Map Routes
        // Method to draw polylines on the map
        public void DrawPolylinesOnMap(Microsoft.Maui.Controls.Maps.Map map)
        {
            try
            {
                //Creating a location and mapspan to set maps initial starting point.
                var location = new Location(-38.069465, 177.264926);
                var mapSpan = new MapSpan(location, 0.005, 0.005);
                map.MoveToRegion(mapSpan);

                // Read locations from CSV file
                var locations = ReadLocationsFromCsv();

                // Convert locations from CSV file to polylines
                var polylines = ConvertToMapPolylines(locations);

                // Add polylines to the map
                foreach (var polyline in polylines)
                {
                    map.MapElements.Add(polyline);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., logging or showing a message to the user)
                System.Diagnostics.Debug.WriteLine($"Error drawing polylines: {ex.Message}");
            }
        }
        #endregion


    }
}
