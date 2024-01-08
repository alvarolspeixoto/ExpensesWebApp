using ExpensesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesWebApp.Data
{
    public class ExpensesAppDbContext : DbContext
    {
        public ExpensesAppDbContext(DbContextOptions<ExpensesAppDbContext> options) : base(options)
        {

        }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                        .HasIndex(e => e.Name)
                        .IsUnique();

            modelBuilder.Entity<Group>()
                        .HasIndex(e => e.Name)
                        .IsUnique();
        }

    }
}
