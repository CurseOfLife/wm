using Api.Configurations.Entities;
using Data.Configurations;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class WmContext : IdentityDbContext<ApiUser>
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
            base.OnModelCreating(builder);

            //seeding the database with roles: admin, user android, user web
            //seeding the databse with initial measuring points for testing purposes
            //seeding the database with initial water meters for testing purposes
            builder.ApplyConfiguration(new RoleConfig());
            builder.ApplyConfiguration(new MeasuringPointConfig());
            builder.ApplyConfiguration(new WaterMeterConfig());
           
        }
    }
}
