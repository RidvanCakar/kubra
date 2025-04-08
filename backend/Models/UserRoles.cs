using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend.Models
{
    public class UserRoles
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public object UserId { get; internal set; }

        public int RoleID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("RoleID")]
        public Roles Role { get; set; }

        
    }
}