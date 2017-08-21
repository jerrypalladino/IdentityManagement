using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using IdentityMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityMgmt.Areas.IdentityManagement.Helpers
{
    public class IdentityHelper
    {
        internal static void SeedIdentities(IdentityMgmtDbContext context)
        {
            string userName = "admin@admin.com";
            string password = "admin09";
            var userManager = new IdentityMgmtUserManager(new IdentityMgmtUserStore(context));
            var roleManager = new IdentityMgmtRoleManager(new IdentityMgmtRoleStore(context));
            if (!roleManager.RoleExists("Administrator"))
            {
                var roleresult = roleManager.Create(new IdentityMgmtRole("Administrator"));
            }
            IdentityMgmtUser user = userManager.FindByName(userName);
            if (user == null)
            {
                user = new IdentityMgmtUser()
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