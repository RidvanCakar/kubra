using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Models;

namespace Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        public float Price { get; set; }
      
        public string ImageUrl { get; set; } = string.Empty;

        public int Stock { get; set; }

        public int CategoryId { get; set; } 

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}