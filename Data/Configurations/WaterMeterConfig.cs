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
    //preconfiguring water meter table 
    public class WaterMeterConfig : IEntityTypeConfiguration<WaterMeter>
    {
        public void Configure(EntityTypeBuilder<WaterMeter> builder)
        {
            builder.HasData(
                new WaterMeter
                {
                    Id = 1,
                    Code = "123",
                    MaxValue = 100,
                    IsActive = true,
                    MeasuringPointId = 1
                },
                new WaterMeter
                {
                    Id = 2,
                    Code = "234",
                    MaxValue = 100,
                    IsActive = true,
                    MeasuringPointId = 2
                },
                new WaterMeter
                {
                    Id = 3,
                    Code = "345",
                    MaxValue = 100,
                    IsActive = true,
                    MeasuringPointId = 3
                }
                );
        }
    }
}
