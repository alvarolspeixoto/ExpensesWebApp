using ExpensesWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ExpensesWebApp.Data
{
    public class ExpensesAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public ExpensesAppDbContext(DbContextOptions<ExpensesAppDbContext> options) : base(options)
        {

        }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>()
                        .HasOne(e => e.Category)
                        .WithMany()
                        .HasForeignKey(e => e.CategoryId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>()
                        .HasIndex(e => e.Name)
                        .IsUnique();

            modelBuilder.Entity<Group>()
                        .HasIndex(e => e.Name)
                        .IsUnique();

            modelBuilder.Entity<Group>()
                        .HasOne(e => e.User)
                        .WithMany()
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
