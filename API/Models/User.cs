using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User
    {
        public bool IsPremium { get; set; } = false;
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        
    }
}
