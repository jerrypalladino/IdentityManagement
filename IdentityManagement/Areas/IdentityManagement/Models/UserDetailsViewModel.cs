using $safeprojectname$.Models;

namespace $safeprojectname$.Areas.IdentityManagement.Models
{
    public class UserDetailsViewModel
    {
        public $safeprojectname$User User { get; set; }
        public LockoutViewModel Lockout { get; set; }
    }
}