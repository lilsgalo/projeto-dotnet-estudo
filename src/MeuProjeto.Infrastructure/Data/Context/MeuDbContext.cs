using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Infrastructure.Data.Context
{
    public class MeuDbContext : IdentityDbContext<CustomUser, CustomRole, Guid, CustomUserClaim, CustomUserRole, CustomUserLogin, CustomRoleClaim, CustomUserToken>
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }

        public DbSet<Picture> Images { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Text> Texts { get; set; }
        public DbSet<UserManual> UserManuals { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomUser>().HasOne(r => r.Image).WithOne().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CustomUser>().HasOne(r => r.Icon).WithOne().OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CustomUser>().HasMany(p => p.UserRoles).WithOne().HasForeignKey(p => p.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomRole>().HasMany(r => r.UserRoles).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomUser>().HasMany(e => e.Claims).WithOne().HasForeignKey(e => e.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomUserRole>().HasOne(r => r.User).WithMany(u => u.UserRoles).HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CustomUserRole>().HasOne(r => r.Role).WithMany(u => u.UserRoles).HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserManual>().HasOne(r => r.LastReviewer).WithMany().HasForeignKey(r => r.LastReviewerId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserManual>().HasIndex(u => u.Code).IsUnique();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}