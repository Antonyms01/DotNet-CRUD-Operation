using CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD
{
    public class CRUDDBContext:DbContext
    {
        public CRUDDBContext(DbContextOptions<CRUDDBContext> options) : base(options) 
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the one-to-many relationship between Category and Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)               // Each Product has one Category
                .WithMany(c => c.Products)             // A Category can have many Products
                .HasForeignKey(p => p.Categoryid);     // Foreign key in Product
        }
    }
}
