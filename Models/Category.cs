using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRUD.Models
{
    public class Category
    {
        [Key]

        public int? Categoryid { get; set; } 
        public string? Categoryname { get; set; }

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}
