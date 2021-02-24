using Microsoft.EntityFrameworkCore;
using DataAccessLibrary.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ESOCompanion.Data.DBContexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public override DbSet<IdentityUserToken<string>> UserTokens { get; set; }
        public override DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
        public override DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }
        public override DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
        public override DbSet<IdentityUserRole<string>> UserRoles { get; set; }
        public DbSet<IdentityUser<string>> IdentityUsers { get; set; }
        public DbSet<IdentityRole<string>> Roles { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> MyUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            modelBuilder.Entity<UserGroup>().ToTable("UserGroups");
            modelBuilder.Entity<User>().ToTable("Users");

            // Configure Primary Keys  

            modelBuilder.Entity<UserGroup>().HasKey(ug => ug.Id).HasName("PK_UserGroups");
            modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("PK_Users");

            // Configure indexes  

            modelBuilder.Entity<UserGroup>().HasIndex(ug => ug.Name).IsUnique().HasDatabaseName("Idx_Name");
            modelBuilder.Entity<User>().HasIndex(u => u.FirstName).HasDatabaseName("Idx_FirstName");
            modelBuilder.Entity<User>().HasIndex(u => u.LastName).HasDatabaseName("Idx_LastName");

            // User Groups
            modelBuilder.Entity<UserGroup>().Property(ug => ug.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<UserGroup>().Property(ug => ug.Name).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<UserGroup>().Property(ug => ug.CreationDateTime).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<UserGroup>().Property(ug => ug.LastUpdateDateTime).HasColumnType("datetime").IsRequired(false);
            // Users
            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.UserGroupId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.CreationDateTime).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastUpdateDateTime).HasColumnType("datetime").IsRequired(false);

            // Configure relationships  
            modelBuilder.Entity<User>().HasOne<UserGroup>().WithMany().HasPrincipalKey(ug => ug.Id).HasForeignKey(u => u.Id).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Users_UserGroups_UserGroupId");

            /// AspNet

            // Map entities to tables  
            modelBuilder.Entity<IdentityUser<string>>().ToTable("AspNetUsers");
            modelBuilder.Entity<IdentityRole<string>>().ToTable("AspNetRoles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens");

            // Configure Primary Keys  
            modelBuilder.Entity<IdentityUser<string>>().HasKey(iu => iu.Id).HasName("PK_AspNetUsers");
            modelBuilder.Entity<IdentityRole<string>>().HasKey(r => r.Id).HasName("PK_AspNetRoles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasKey(rc => rc.Id).HasName("PK_AspNetRoleClaims");
            modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(uc => uc.Id).HasName("PK_AspNetUserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(ul => new { ul.LoginProvider, ul.ProviderKey }).HasName("PK_AspNetUserLogins");
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(ur => new { ur.UserId, ur.RoleId }).HasName("PK_AspNetUserRoles");
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name }).HasName("PK_AspNetUserTokens");

            // Configure indexes  
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasIndex(rc => rc.RoleId).HasDatabaseName("IX_AspNetRoleClaims_RoleId");
            modelBuilder.Entity<IdentityRole<string>>().HasIndex(r => r.NormalizedName).IsUnique().HasDatabaseName("RoleNameIndex").HasFilter("[NormalizedName] IS NOT NULL");
            modelBuilder.Entity<IdentityUserClaim<string>>().HasIndex(uc => uc.UserId).HasDatabaseName("IX_AspNetUserClaims_UserId");
            modelBuilder.Entity<IdentityUserLogin<string>>().HasIndex(ul => ul.UserId).HasDatabaseName("IX_AspNetUserLogins_UserId");
            modelBuilder.Entity<IdentityUserRole<string>>().HasIndex(ur => ur.RoleId).HasDatabaseName("IX_AspNetUserRoles_RoleId");
            modelBuilder.Entity<IdentityUser<string>>().HasIndex(iu => iu.NormalizedEmail).HasDatabaseName("EmailIndex");
            modelBuilder.Entity<IdentityUser<string>>().HasIndex(iu => iu.NormalizedUserName).IsUnique().HasDatabaseName("UserNameIndex").HasFilter("[NormalizedUserName] IS NOT NULL");

            // Application User
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.Id).HasColumnType("nvarchar(256)").IsRequired();
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.UserName).HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.NormalizedUserName).HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.Email).HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.NormalizedEmail).HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.EmailConfirmed).HasColumnType("tinyint").IsRequired();
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.PasswordHash).HasColumnType("nvarchar(256)").IsRequired(false);
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.SecurityStamp).HasColumnType("nvarchar(256)").IsRequired(false);
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.ConcurrencyStamp).HasColumnType("nvarchar(256)").IsRequired(false);
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.PhoneNumber).HasColumnType("nvarchar(256)").IsRequired(false);
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.PhoneNumberConfirmed).HasColumnType("tinyint").IsRequired();
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.TwoFactorEnabled).HasColumnType("tinyint").IsRequired();
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.LockoutEnd).HasColumnType("datetime").IsRequired(false);
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.LockoutEnabled).HasColumnType("tinyint").IsRequired();
            modelBuilder.Entity<IdentityUser<string>>().Property(iu => iu.AccessFailedCount).HasColumnType("int").IsRequired();

            // Roles
            modelBuilder.Entity<IdentityRole<string>>().Property(r => r.Id).HasColumnType("nvarchar(256)").IsRequired();
            modelBuilder.Entity<IdentityRole<string>>().Property(r => r.Name).HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<IdentityRole<string>>().Property(r => r.NormalizedName).HasColumnType("nvarchar(256)").IsRequired(false).HasMaxLength(256);
            modelBuilder.Entity<IdentityRole<string>>().Property(r => r.ConcurrencyStamp).HasColumnType("nvarchar(256)").IsRequired(false);
            
            // User Roles
            modelBuilder.Entity<IdentityUserRole<string>>().Property(ur => ur.UserId).HasColumnType("nvarchar(256)").IsRequired();
            modelBuilder.Entity<IdentityUserRole<string>>().Property(ur => ur.RoleId).HasColumnType("nvarchar(256)").IsRequired();

            // Roles Claims
            modelBuilder.Entity<IdentityRoleClaim<string>>().Property(rc => rc.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<IdentityRoleClaim<string>>().Property(rc => rc.RoleId).HasColumnType("nvarchar(256)").IsRequired();
            modelBuilder.Entity<IdentityRoleClaim<string>>().Property(rc => rc.ClaimType).HasColumnType("nvarchar(256)").IsRequired(false);
            modelBuilder.Entity<IdentityRoleClaim<string>>().Property(rc => rc.ClaimValue).HasColumnType("nvarchar(256)").IsRequired(false);

            // User Claims
            modelBuilder.Entity<IdentityUserClaim<string>>().Property(uc => uc.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<IdentityUserClaim<string>>().Property(uc => uc.UserId).HasColumnType("nvarchar(256)").IsRequired();
            modelBuilder.Entity<IdentityUserClaim<string>>().Property(uc => uc.ClaimType).HasColumnType("nvarchar(256)").IsRequired(false);
            modelBuilder.Entity<IdentityUserClaim<string>>().Property(uc => uc.ClaimValue).HasColumnType("nvarchar(256)").IsRequired(false);

            // User Logins
            modelBuilder.Entity<IdentityUserLogin<string>>().Property(ul => ul.LoginProvider).HasColumnType("nvarchar(128)").IsRequired().HasMaxLength(128);
            modelBuilder.Entity<IdentityUserLogin<string>>().Property(ul => ul.ProviderKey).HasColumnType("nvarchar(128)").IsRequired().HasMaxLength(128);
            modelBuilder.Entity<IdentityUserLogin<string>>().Property(ul => ul.ProviderDisplayName).HasColumnType("nvarchar(256)").IsRequired(false);
            modelBuilder.Entity<IdentityUserLogin<string>>().Property(ul => ul.UserId).HasColumnType("nvarchar(256)").IsRequired();

            // User Tokens
            modelBuilder.Entity<IdentityUserToken<string>>().Property(ut => ut.UserId).HasColumnType("nvarchar(256)").IsRequired();
            modelBuilder.Entity<IdentityUserToken<string>>().Property(ut => ut.LoginProvider).HasColumnType("nvarchar(256)").IsRequired().HasMaxLength(128);
            modelBuilder.Entity<IdentityUserToken<string>>().Property(ut => ut.Name).HasColumnType("nvarchar(256)").IsRequired().HasMaxLength(128);
            modelBuilder.Entity<IdentityUserToken<string>>().Property(ut => ut.Value).HasColumnType("nvarchar(256)").IsRequired(false);

            // Configure relationships  
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasOne<IdentityRole<string>>().WithMany().HasPrincipalKey(r => r.Id).HasForeignKey(rc => rc.RoleId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
            modelBuilder.Entity<IdentityUserClaim<string>>().HasOne<IdentityUser<string>>().WithMany().HasPrincipalKey(uc => uc.Id).HasForeignKey(iu => iu.UserId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
            modelBuilder.Entity<IdentityUserLogin<string>>().HasOne<IdentityUser<string>>().WithMany().HasPrincipalKey(ul => ul.Id).HasForeignKey(iu => iu.UserId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
            modelBuilder.Entity<IdentityUserRole<string>>().HasOne<IdentityRole<string>>().WithMany().HasPrincipalKey(ur => ur.Id).HasForeignKey(r => r.RoleId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId");
            modelBuilder.Entity<IdentityUserRole<string>>().HasOne<IdentityUser<string>>().WithMany().HasPrincipalKey(ur => ur.Id).HasForeignKey(iu => iu.UserId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId");
            modelBuilder.Entity<IdentityUserRole<string>>().HasOne<IdentityUser<string>>().WithMany().HasPrincipalKey(ut => ut.Id).HasForeignKey(iu => iu.UserId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
        }
    }
}