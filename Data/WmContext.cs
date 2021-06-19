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
    //TODO
    //cascade or restricted check later and change then migrate

    public class WmContext : IdentityDbContext<User>
    {
       
        public DbSet<Measurement> Measurements { get; set; } //Measurements table 
        public DbSet<WaterMeter> WaterMeters { get; set; } //WaterMeters table 
        public DbSet<MeasuringPoint> MeasuringPoints { get; set; } //MeasuringPoints table 
        public DbSet<ReadingStatus> ReadingStatuses { get; set; } //ReadingStatuses table 

        //passing configurations to the IdentityDBContext constructor
        public WmContext(DbContextOptions opt) : base(opt)
        {

        }

       //preconfiguring database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seeding the database with roles: admin, user android, user web
            //seeding the databse with initial measuring points for testing purposes
            //seeding the database with initial water meters for testing purposes
            builder.ApplyConfiguration(new RoleConfig());
            builder.ApplyConfiguration(new MeasuringPointConfig());
            builder.ApplyConfiguration(new WaterMeterConfig());
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new ReadingStatusesConfig());
        }
    }
}
