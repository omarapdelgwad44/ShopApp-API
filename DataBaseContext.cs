using Microsoft.EntityFrameworkCore;
using ShopApp_API.Models;

namespace ShopApp_API
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Item and Categories
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Configure delete behavior


            {
                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(u => u.Id);
                    entity.Property(u => u.Email)
                          .IsRequired()
                          .HasMaxLength(150);
                    entity.HasIndex(u => u.Email)
                          .IsUnique();
                    entity.Property(u => u.Name)
                          .IsRequired()
                          .HasMaxLength(100);
                    entity.Property(u => u.UserType)
                          .IsRequired();
                });
            }
        }
        }
    }
