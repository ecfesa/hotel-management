using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace hotel_management.Models
{
    public class CustomerViewModel : StandardViewModel
    {
        
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public int PersonID { get; set; }

    }
}