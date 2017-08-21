using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;

namespace $safeprojectname$.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    // New derived classes
    public class $safeprojectname$UserRole : IdentityUserRole<int>
    {
    }

    public class $safeprojectname$UserClaim : IdentityUserClaim<int>
    {
    }

    public class $safeprojectname$UserLogin : IdentityUserLogin<int>
    {
    }

    public class $safeprojectname$Role : IdentityRole<int, $safeprojectname$UserRole>
    {
        public $safeprojectname$Role() { }
        public $safeprojectname$Role(string name) { Name = name; }
    }

    public class $safeprojectname$UserStore : UserStore<$safeprojectname$User, $safeprojectname$Role, int,
        $safeprojectname$UserLogin, $safeprojectname$UserRole, $safeprojectname$UserClaim>
    {
        public $safeprojectname$UserStore($safeprojectname$DbContext context) : base(context)
        {
        }
    }
    //internal class $safeprojectname$RoleStore<T> : IRoleStore<$safeprojectname$Role, int>
    public class $safeprojectname$RoleStore : RoleStore<$safeprojectname$Role, int, $safeprojectname$UserRole>
    {
        public $safeprojectname$RoleStore($safeprojectname$DbContext context) : base(context)
        {
        }
    }

    public class $safeprojectname$User : IdentityUser<int, $safeprojectname$UserLogin, $safeprojectname$UserRole, $safeprojectname$UserClaim>
    {
        public DateTime? ActiveUntil;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync($safeprojectname$UserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class $safeprojectname$DbContext : IdentityDbContext<$safeprojectname$User, $safeprojectname$Role, int,
        $safeprojectname$UserLogin, $safeprojectname$UserRole, $safeprojectname$UserClaim>
    {
        public $safeprojectname$DbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.HasDefaultSchema("adm").Entity<$safeprojectname$User>().ToTable("User");
            modelBuilder.HasDefaultSchema("adm").Entity<$safeprojectname$Role>().ToTable("Role");
            modelBuilder.HasDefaultSchema("adm").Entity<$safeprojectname$UserRole>().ToTable("UserRole");
            modelBuilder.HasDefaultSchema("adm").Entity<$safeprojectname$UserClaim>().ToTable("UserClaim");
            modelBuilder.HasDefaultSchema("adm").Entity<$safeprojectname$UserLogin>().ToTable("UserLogin");
        }

        public static $safeprojectname$DbContext Create()
        {
            return new $safeprojectname$DbContext();
        }
    }

    
}