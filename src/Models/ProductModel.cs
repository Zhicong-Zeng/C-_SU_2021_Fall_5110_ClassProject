using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Products model will perform get, set on the products from /data/products.json and serialize the results
    /// </summary>
    public class ProductModel
    {
        // get set method for JSON attribute ID
        public string Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "Artist name should have a length of more than {2} and less than {1}")]
        // get set method for JSON attribute Artist
        public string Artist { get; set; }
        
        [Required]
        [Url]
        [JsonPropertyName("img")]
        // get set method for JSON attribute Image
        public string Image { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "The Title should have a length of more than {2} and less than {1}")]
        // get set method for JSON attribute Title
        public string Title { get; set; }

        [Required]
        // get set method for JSON attribute Description
        public string Description { get; set; }

        // get set method for JSON attribute Ratings
        public int[] Ratings { get; set; }

        // get set method for JSON attribute Comments
        public string[] Comments { get; set; }

        // JSON Serializar that overrides the ToString function
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
}