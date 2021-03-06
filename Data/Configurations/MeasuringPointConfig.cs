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
    public class MeasuringPointConfig : IEntityTypeConfiguration<MeasuringPoint>
    {
        //preconfiguring measuring point table table 
        public void Configure(EntityTypeBuilder<MeasuringPoint> builder)
        {
            builder.HasData(
               new MeasuringPoint
               {
                   Id = 1,
                   Street = "Test Street Name One",
                   Number = "1",
                   Place = "Test Place Name One",
                   Description = "Name Surname One",
                   UserId = "f94d651e-3e4a-429d-b987-fa0b9f7be9ea"

               },
               new MeasuringPoint
               {
                   Id = 2,
                   Street = "Test Street Name Two",
                   Number = "2",
                   Place = "Test Place Name Two",
                   Description = "Name Surname Two",
                   UserId = "f94d651e-3e4a-429d-b987-fa0b9f7be9ea"
               },
               new MeasuringPoint
               {
                   Id = 3,
                   Street = "Test Street Name Three",
                   Number = "3",
                   Place = "Test Place Name Three",
                   Description = "Name Surname Three",
                   UserId = "f94d651e-3e4a-429d-b987-fa0b9f7be9ea"
               }
               );
        }
    }
}
