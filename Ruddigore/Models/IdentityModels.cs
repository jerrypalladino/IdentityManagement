using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;

namespace IdentityMgmt.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    // New derived classes
    public class IdentityMgmtUserRole : IdentityUserRole<int>
    {
    }

    public class IdentityMgmtUserClaim : IdentityUserClaim<int>
    {
    }

    public class IdentityMgmtUserLogin : IdentityUserLogin<int>
    {
    }

    public class IdentityMgmtRole : IdentityRole<int, IdentityMgmtUserRole>
    {
        public IdentityMgmtRole() { }
        public IdentityMgmtRole(string name) { Name = name; }
    }

    public class IdentityMgmtUserStore : UserStore<IdentityMgmtUser, IdentityMgmtRole, int,
        IdentityMgmtUserLogin, IdentityMgmtUserRole, IdentityMgmtUserClaim>
    {
        public IdentityMgmtUserStore(IdentityMgmtDbContext context) : base(context)
        {
        }
    }
    //internal class IdentityMgmtRoleStore<T> : IRoleStore<IdentityMgmtRole, int>
    public class IdentityMgmtRoleStore : RoleStore<IdentityMgmtRole, int, IdentityMgmtUserRole>
    {
        public IdentityMgmtRoleStore(IdentityMgmtDbContext context) : base(context)
        {
        }
    }

    public class IdentityMgmtUser : IdentityUser<int, IdentityMgmtUserLogin, IdentityMgmtUserRole, IdentityMgmtUserClaim>
    {
        public DateTime? ActiveUntil;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(IdentityMgmtUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class IdentityMgmtDbContext : IdentityDbContext<IdentityMgmtUser, IdentityMgmtRole, int,
        IdentityMgmtUserLogin, IdentityMgmtUserRole, IdentityMgmtUserClaim>
    {
        public IdentityMgmtDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.HasDefaultSchema("adm").Entity<IdentityMgmtUser>().ToTable("User");
            modelBuilder.HasDefaultSchema("adm").Entity<IdentityMgmtRole>().ToTable("Role");
            modelBuilder.HasDefaultSchema("adm").Entity<IdentityMgmtUserRole>().ToTable("UserRole");
            modelBuilder.HasDefaultSchema("adm").Entity<IdentityMgmtUserClaim>().ToTable("UserClaim");
            modelBuilder.HasDefaultSchema("adm").Entity<IdentityMgmtUserLogin>().ToTable("UserLogin");
        }

        public static IdentityMgmtDbContext Create()
        {
            return new IdentityMgmtDbContext();
        }
    }

    
}