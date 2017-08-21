using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using $safeprojectname$.Areas.IdentityManagement.Models;
using $safeprojectname$.Models;

namespace $safeprojectname$.Areas.IdentityManagement.Helpers
{
    public class IdentityModelHelper
    {
        private readonly $safeprojectname$UserManager _userManager;
        private $safeprojectname$RoleManager _roleManager;
        public IdentityModelHelper($safeprojectname$UserManager userManager, $safeprojectname$RoleManager roleManager)
        {
            Contract.Assert(null != userManager);
            Contract.Assert(null != roleManager);
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserDetailsViewModel> GetUserDetailsViewModel(int id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var detailsModel = new UserDetailsViewModel() { User = user, Lockout = new LockoutViewModel() };
            var isLocked = await _userManager.IsLockedOutAsync(user.Id);
            detailsModel.Lockout.Status = isLocked ? LockoutStatus.Locked : LockoutStatus.Unlocked;
            if (detailsModel.Lockout.Status == LockoutStatus.Locked)
            {
                detailsModel.Lockout.LockoutEndDate = (await _userManager.GetLockoutEndDateAsync(user.Id)).DateTime;
            }
            return detailsModel;
        }
    }
}