using Domain;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        //preconfiguring user table 
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
        }
    }
}
