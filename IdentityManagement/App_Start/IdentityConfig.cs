using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using $safeprojectname$.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace $safeprojectname$
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public class $safeprojectname$UserManager : UserManager<$safeprojectname$User, int>
    {
        public $safeprojectname$UserManager(IUserStore<$safeprojectname$User, int> store)
            : base(store)
        {
        }

        public static $safeprojectname$UserManager Create(IdentityFactoryOptions<$safeprojectname$UserManager> options, IOwinContext context)
        {
            var manager = new $safeprojectname$UserManager(new $safeprojectname$UserStore(context.Get<$safeprojectname$DbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<$safeprojectname$User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                // RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<$safeprojectname$User, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<$safeprojectname$User, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<$safeprojectname$User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public virtual async Task<IdentityResult> LockUserAccount(int userId, int? forDays)
        {
            var result = await SetLockoutEnabledAsync(userId, true);
            if (result.Succeeded)
            {
                if (forDays.HasValue)
                {
                    result = await SetLockoutEndDateAsync(userId, DateTimeOffset.UtcNow.AddDays(forDays.Value));
                }
                else
                {
                    result = await SetLockoutEndDateAsync(userId, DateTimeOffset.MaxValue);
                }
            }
            return result;
        }

        public virtual async Task<IdentityResult> UnlockUserAccount(int userId)
        {
            var result = await SetLockoutEnabledAsync(userId, false);
            if (result.Succeeded)
            {
                await ResetAccessFailedCountAsync(userId);
            }
            return result;
        }

    }

    // Configure the application sign-in manager which is used in this application.
    public class $safeprojectname$SignInManager : SignInManager<$safeprojectname$User, int>
    {
        public $safeprojectname$SignInManager($safeprojectname$UserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync($safeprojectname$User user)
        {
            return user.GenerateUserIdentityAsync(($safeprojectname$UserManager)UserManager);
        }

        public static $safeprojectname$SignInManager Create(IdentityFactoryOptions<$safeprojectname$SignInManager> options, IOwinContext context)
        {
            return new $safeprojectname$SignInManager(context.GetUserManager<$safeprojectname$UserManager>(), context.Authentication);
        }
    }

    public class $safeprojectname$RoleManager : RoleManager<$safeprojectname$Role, int>
    {
        public $safeprojectname$RoleManager(IRoleStore<$safeprojectname$Role, int> roleStore)
             : base(roleStore)
        {
        }

        public static $safeprojectname$RoleManager Create(IdentityFactoryOptions<$safeprojectname$RoleManager> options, IOwinContext context)
        {
            var manager = new $safeprojectname$RoleManager(new $safeprojectname$RoleStore(context.Get<$safeprojectname$DbContext>()));

            return manager;
        }

        //internal static void SeedIdentities(IOwinContext context)
        //{
        //    string userName = "admin@admin.com";
        //    string password = "admin09";
        //    var userManager = new $safeprojectname$UserManager(new $safeprojectname$UserStore(context.Get<$safeprojectname$DbContext>()));
        //    var roleManager = new $safeprojectname$RoleManager(new $safeprojectname$RoleStore(context.Get<$safeprojectname$DbContext>()));
        //    if (!roleManager.RoleExists("Administrator"))
        //    {
        //        var roleresult = roleManager.Create(new $safeprojectname$Role("Administrator"));
        //    }
        //    $safeprojectname$User user = userManager.FindByName(userName);
        //    if (user == null)
        //    {
        //        user = new $safeprojectname$User()
        //        {
        //            UserName = userName,
        //            Email = userName,
        //            EmailConfirmed = true
        //        };
        //        IdentityResult userResult = userManager.Create(user, password);
        //        if (userResult.Succeeded)
        //        {
        //            var result = userManager.AddToRole(user.Id, "Administrator");
        //        }
        //    }

        //}
    }

    //public class $safeprojectname$RoleManager : RoleManager<IdentityRole>
    //{
    //    public $safeprojectname$RoleManager($safeprojectname$RoleStore<$safeprojectname$Role> store) : base(store)
    //    {
    //    }
    //    public static $safeprojectname$RoleManager Create(IdentityFactoryOptions<$safeprojectname$RoleManager> options, IOwinContext context)
    //    {
    //        var roleStore = new RoleStore<IdentityRole>(context.Get<$safeprojectname$DbContext>());
    //        return new $safeprojectname$RoleManager(roleStore);
    //    }
    //}
}
