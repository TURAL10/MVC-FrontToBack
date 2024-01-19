using HomeTaskkMVC4.Models;
using Microsoft.AspNetCore.Identity;

namespace HomeTaskkMVC4.ViewModels.AdminRole
{
    public class UpdateRoleVM
    {
        public List<IdentityRole> Roles { get; set; }

        public IList<string> UserRoles { get; set;}

        public AppUser User { get; set; }
    }
}
