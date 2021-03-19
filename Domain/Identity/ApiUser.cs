using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class ApiUser : IdentityUser
    {
        public string FirseName { get; set; }
        public string LastName { get; set; }
    }
}
