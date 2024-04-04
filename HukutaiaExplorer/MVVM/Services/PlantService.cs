using HukutaiaExplorer.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HukutaiaExplorer.MVVM.Services
{
    public class PlantService
    {
        public async Task<List<Plant>> GetPlants()
        {
            List<Plant>? plants = new List<Plant>();

            try
            {
                // Load JSON data from Maui asset
                using (var stream = await FileSystem.OpenAppPackageFileAsync("plantdata.json"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        // Read JSON data as string
                        string json = await reader.ReadToEndAsync();

                        // Deserialize JSON data into a list of Plant objects
                        plants = JsonSerializer.Deserialize<List<Plant>>(json);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"Error loading plant data: {ex.Message}");
            }

            if (plants.Count <= 0)
            {
                //Null value return if plants list didnt populate
                return null;
            }

            else
            {
                //Returning the new list of plants
                return plants;
            }
        }
    }
}
