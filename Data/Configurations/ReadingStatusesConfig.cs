using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class ReadingStatusesConfig : IEntityTypeConfiguration<ReadingStatus>
    {
        //preconfiguring measuring statuses table 
        public void Configure(EntityTypeBuilder<ReadingStatus> builder)
        {
            builder.HasData(
                new ReadingStatus
                {
                    Id = 1,
                    Value = "Uspesno"
                },
                new ReadingStatus
                {
                    Id = 2,
                    Value = "Zakljucana santa"
                },
                new ReadingStatus
                {
                    Id = 3,
                    Value = "Prljavo brojilo"
                },
                new ReadingStatus
                {
                    Id = 4,
                    Value = "Auto na santu"
                }
                );
        }
    }
}


