using System.Text.Json;
using System.Text.Json.Serialization;

namespace ContoseCrafts.Models
{
    public class Product
    {
        public string ID { get; set; }

        public string Maker { get; set; }

        [JsonPropertyName("img")]
        public string Image { get; set; }

        public string URL { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int[] Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Product>(this); // convert the JSON representation of the product to string

    }
}
