using System.ComponentModel.DataAnnotations.Schema;
using Models;
using System.ComponentModel.DataAnnotations;


namespace backend.Models
{
    [Table("ShoppingCard")]
    public class ShoppingCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Price { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; } = null;

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; } = null;

        public string ImageUrl { get; set; } = string.Empty;

        public int quantity{get;set;}

    }

}
