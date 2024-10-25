using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRUD.Models
{
    public class Product
    {
        [Key]
        public int? Productid { get; set; }
        public string? Productname {  get; set; }
        public int? Categoryid { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
