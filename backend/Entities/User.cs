using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    [Table("Users")]

    public class User
    {
        [Key]
        public int ID { get; set; }
    
        public string? Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

}