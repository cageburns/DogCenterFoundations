using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DogCenterFoundations.Models.Entities;

namespace DogCenterFoundations.Data
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

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    ServiceName = "Puppy Group Training",
                    ServiceType = "School",
                    Price = 550,
                    Description = "10 group lessons for puppies up to 8 months. Fixed schedule, covering basic obedience, socialization, and building a strong relationship with your puppy."
                },
                new Service
                {
                    Id = 2,
                    ServiceName = "Nail Trimming",
                    ServiceType = "Salon",
                    Price = 30,
                    Description = "Professional nail trimming to keep your dog's paws healthy and comfortable."
                },
                new Service
                {
                    Id = 3,
                    ServiceName = "Daycare",
                    ServiceType = "Hotel",
                    Price = 15,
                    Description = "Supervised daytime care in a safe and welcoming environment. Available daily from 08:00 to 19:00. Price per day."
                },
                new Service
                {
                    Id = 4,
                    ServiceName = "VIP Subscription",
                    ServiceType = "Misc",
                    Price = 600,
                    Description = "Monthly subscription including unlimited daycare and overnight stays, plus a 5% discount on all other services."
                }
            );

            modelBuilder.Entity<Owner>().HasData(
                new Owner
                {
                    Id = 1,
                    FirstName = "Elena",
                    LastName = "Fischer",
                    PhoneNumber = "+4935900000000",
                    Email = "elena.fischer@example.com",
                    Notes = "Responsible owner. Actively involved in training and socialization. No outstanding payments."
                }
            );

            modelBuilder.Entity<Dog>().HasData(
                new Dog
                {
                    Id = 1,
                    Name = "Onyx",
                    Breed = "German Shepherd",
                    Birthday = new DateTime(2023, 3, 14),
                    MicrochipId = "276098100123456",
                    OwnerId = 1,
                    RegistrationDate = new DateTime(2025, 6, 10),
                    IsVip = true,
                    Notes = "Friendly, energetic, and enthusiastic about training. Loves treats. Crazy about fetch."
                }
            );

            modelBuilder.Entity<MedicalRecord>().HasData(
                new MedicalRecord
                {
                    Id = 1,
                    DogId = 1,
                    RecordType = "Vaccination",
                    Name = "Rabies Vaccination",
                    Date = new DateTime(2025, 5, 12),
                    Brand = "Rabisin",
                    NextDueDate = new DateTime(2028, 5, 12),
                    Notes = "Routine vaccination completed without complications. Recommended supplements for joint health."
                }
            );
        }
    }
}
