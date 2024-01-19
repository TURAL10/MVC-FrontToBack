using Microsoft.AspNetCore.Identity;

namespace HomeTaskkMVC4.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }

        
    }
}
