using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Context
{
    public class eCommerceContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Roles> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<ShoppingCard> ShoppingCards {get;set;}
        
        public DbSet<Order> Orders {get;set;}
        
        public eCommerceContext(DbContextOptions<eCommerceContext> options) : base(options) { }

    }


}