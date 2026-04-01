using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace YeniKitapKirtasiyeWebApp.Models
{
    public partial class YeniKitapKirtasiyeDBModel : DbContext
    {
        public YeniKitapKirtasiyeDBModel()
            : base("name=DBModel")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
            .HasKey(od => new { od.OrderID, od.ProductID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
