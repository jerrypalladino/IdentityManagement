using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using $safeprojectname$.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace $safeprojectname$.Areas.IdentityManagement.Helpers
{
    public class IdentityHelper
    {
        internal static void SeedIdentities($safeprojectname$DbContext context)
        {
            string userName = "admin@admin.com";
            string password = "admin09";
            var userManager = new $safeprojectname$UserManager(new $safeprojectname$UserStore(context));
            var roleManager = new $safeprojectname$RoleManager(new $safeprojectname$RoleStore(context));
            if (!roleManager.RoleExists("Administrator"))
            {
                var roleresult = roleManager.Create(new $safeprojectname$Role("Administrator"));
            }
            $safeprojectname$User user = userManager.FindByName(userName);
            if (user == null)
            {
                user = new $safeprojectname$User()
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };
                IdentityResult userResult = userManager.Create(user, password);
                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Administrator");
                }
            }
            else
            {
                userManager.AddToRole(user.Id, "Administrator");
            }
        }
    }
}