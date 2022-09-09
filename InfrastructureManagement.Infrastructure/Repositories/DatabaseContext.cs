using InfrastructureManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureManagement.Infrastructure.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<TokenAccount> TokenAccounts { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemLog> ItemLogs { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<MapItem> MapItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>()
                        .HasIndex(u => u.Username)
                        .IsUnique();
            builder.Entity<Account>()
                        .HasIndex(u => u.Email)
                        .IsUnique();

            builder.Entity<Category>()
                        .HasIndex(u => u.Code)
                        .IsUnique();

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
