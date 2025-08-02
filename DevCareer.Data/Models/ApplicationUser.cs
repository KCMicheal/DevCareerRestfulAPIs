

using Microsoft.AspNetCore.Identity;

namespace DevCareer.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
    }
}
