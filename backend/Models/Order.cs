using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace backend.Models
{

    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int Price { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; } = null;
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        public int quantity { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string OrderNumber { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    }
}