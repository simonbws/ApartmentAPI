using Microsoft.AspNetCore.Identity;

namespace Apartment_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
