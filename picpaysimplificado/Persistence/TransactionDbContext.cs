using Microsoft.EntityFrameworkCore;
using picpaysimplificado.Entities;
using System.ComponentModel.DataAnnotations;

namespace picpaysimplificado.Persistence
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Transfer> Transactions { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);

                e.Property(u => u.FirstName)
                .IsRequired(true)
                .HasMaxLength(50);

                e.Property<string>(u => u.LastName)
                .IsRequired(true)
                .HasMaxLength(50);

                e.Property(u => u.Email).HasMaxLength(50);

                e.HasIndex(u => u.Email)
                .IsUnique();

                e.HasIndex(u => u.Document).IsUnique();
            });

        }
    }
}
