using IdentityMgmt.Models;

namespace IdentityMgmt.Areas.IdentityManagement.Models
{
    public class UserDetailsViewModel
    {
        public IdentityMgmtUser User { get; set; }
        public LockoutViewModel Lockout { get; set; }
    }
}