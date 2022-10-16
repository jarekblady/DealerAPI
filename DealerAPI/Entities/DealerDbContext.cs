using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Entities
{
    public class DealerDbContext : DbContext
    {
        public DealerDbContext(DbContextOptions<DealerDbContext> options) : base(options)
        {

        }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired();
            //Role
            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();
            //Dealer
            modelBuilder.Entity<Dealer>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(25);
            //Car
            modelBuilder.Entity<Car>()
                .Property(d => d.Make)
                .IsRequired()
                .HasMaxLength(25); 
            modelBuilder.Entity<Car>()
                .Property(d => d.Model)
                .IsRequired()
                .HasMaxLength(25); 
            modelBuilder.Entity<Car>()
                .Property(d => d.Year)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(d => d.Fuel)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Car>()
                .Property(d => d.EnginePower)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(d => d.Price)
                .IsRequired()
                .HasPrecision(14, 2);
            //Address
            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Address>()
                .Property(a => a.HouseNumber)
                .IsRequired()
                .HasMaxLength(25);
        }
    }



}
