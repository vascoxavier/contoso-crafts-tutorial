using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using ContoseCrafts.Models;

namespace ContoseCrafts.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {   
            // inject the information about the web hosting of the application
            this.WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {   // return the full path to products.json file
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

 
        public IEnumerable<Product> GetProducts()
        {   
            // Opening the products.json file for reading
            using (var jsonFileReader = File.OpenText(this.JsonFileName))
            {   
                // Convert the text representation of a product to Json representation
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }


        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();
            /**
             * TODO: Prevent throw exception when the ID is not found
             */
            var query = products.First(x => x.ID == productId);

            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
               );
            }
        }
    }
}
