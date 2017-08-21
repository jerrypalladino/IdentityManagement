using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Areas.IdentityManagement.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }
}