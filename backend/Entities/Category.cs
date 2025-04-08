using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Category")]
    public class Category
    {

        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }

        [JsonIgnore]

    public List<Product> Products { get; set; } = new List<Product>();

    }
}



