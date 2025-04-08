using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        public int ID { get; set; }

        public string RoleName { get; set; } = string.Empty;

    }
}