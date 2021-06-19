using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Configurations.Entities
{
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        //preconfiguring roles table 
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "AndroidUser",
                    NormalizedName = "ANDROIDUSER"
                },
                new IdentityRole
                {
                    Name = "WebUser",
                    NormalizedName = "WEBUSER"
                },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
        }
    }
}
