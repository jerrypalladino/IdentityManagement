using System;

namespace $safeprojectname$.Areas.IdentityManagement.Models
{
    public class LockoutViewModel
    {
        public LockoutStatus Status { get; set; }
        public DateTime? LockoutEndDate { get; set; }
    }
}