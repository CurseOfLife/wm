using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class WmContext : DbContext
    {
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<WaterMeter> WaterMeters { get; set; }
        public DbSet<MeasuringPoint> MeasuringPoints { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<ReadingStatus> ReadingStatuses { get; set; }

        public WmContext(DbContextOptions opt) : base(opt)
        {

        }

        // Update-database -TargetMigration:0

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<MeasuringPoint>().HasData(
                new MeasuringPoint
                {
                    Id = 1,
                    Street = "Test Street Name One",
                    Number = "1",
                    Place = "Test Place Name One",
                    Description = "Name Surname One"
                },
                new MeasuringPoint
                {
                    Id = 2,
                    Street = "Test Street Name Two",
                    Number = "2",
                    Place = "Test Place Name Two",
                    Description = "Name Surname Two"
                },
                new MeasuringPoint
                {
                    Id = 3,
                    Street = "Test Street Name Three",
                    Number = "3",
                    Place = "Test Place Name Three",
                    Description = "Name Surname Three"
                }
                ); 

            builder.Entity<WaterMeter>().HasData(
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
