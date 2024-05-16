using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotel_management.Models
{
    public class LoginViewModel : StandardViewModel
    {

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IsAdmin { get; set; }
        
    }
}