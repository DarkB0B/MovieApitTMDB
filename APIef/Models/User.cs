using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace APIef.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public bool IsPremium { get; set; } = false;
        public string Password { get; set; }
        public  Role Role { get; set; }     
    }
}
