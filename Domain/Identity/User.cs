using IEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
   //entity User class
   //inherits from Identity user class
   
    //TO DO.. implement empty interface and use that for operations
   
    public class User : IdentityUser, IEntity
    {
        public string FirseName { get; set; }
        public string LastName { get; set; }

       public IList<MeasuringPoint> MeasuringPoints { get; set; }
    }
}
