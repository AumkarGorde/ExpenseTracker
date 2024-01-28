using ExpenseTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure
{
    public class ExpenseTrackerDBContext : DbContext
    {
        public ExpenseTrackerDBContext(DbContextOptions<ExpenseTrackerDBContext> options)
            : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<CategoryUser> CategoryUsers { get; set; }
        public DbSet<Budget> Budget { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<Users>().HasKey(u => u.UserId);

            // Expenses
            modelBuilder.Entity<Expenses>()
                .HasKey(e => e.ExpenseId);
            modelBuilder.Entity<Expenses>()
                .HasOne(e => e.User)
                .WithMany(e => e.Expenses)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<Expenses>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId);

            // Categories
            modelBuilder.Entity<Categories>().HasKey(c => c.CategoryId);

            // CategoryUsers
            modelBuilder.Entity<CategoryUser>()
                .HasKey(cu => new { cu.CategoryId, cu.UserId });
            modelBuilder.Entity<CategoryUser>()
                .HasOne(cu => cu.Category)
                .WithMany(c => c.CategoryUsers)
                .HasForeignKey(cu => cu.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CategoryUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.CategoryUsers)
                .HasForeignKey(cu => cu.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Budget
            modelBuilder.Entity<Budget>()
                .HasKey(b=>b.BudgetId);
            modelBuilder.Entity<Budget>()
                .HasOne(e => e.User)
                .WithOne(u => u.Budget)
                .HasForeignKey<Budget>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
