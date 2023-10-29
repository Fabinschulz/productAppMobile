using Microsoft.EntityFrameworkCore;
using ProductAppMAUI.Domain.Entities;

namespace ProductAppMAUI.Infra.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string conexionDB = $"Filename={ConexionDB.ReturnRoute("product.db")}";
            options.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(col => col.Id);
                entity.Property(col => col.Id).IsRequired().ValueGeneratedOnAdd();
            });
        }

    }
}
