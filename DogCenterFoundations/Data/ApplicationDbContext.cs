using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DogCenter.Models.Entities;

namespace DogCenter.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Owner> Owners { get; set; } = null!;
        public DbSet<Dog> Dogs { get; set; } = null!;
        public DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dog>()
                .HasOne(d => d.Owner)
                .WithMany(o => o.Dogs)
                .HasForeignKey(d => d.OwnerId);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Dog)
                .WithMany(d => d.MedicalRecords)
                .HasForeignKey(m => m.DogId);

            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasPrecision(5, 2);
        }
    }
}
