using Api.IRepository;
using Data;
using Domain;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    //https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    //when using multiple repositories, they all share a single database context.
    public class UnitOfWork : IUnitOfWork
    {
        //context
        private readonly WmContext _context;

        //repositories
        private IGenericRepository<Measurement> _measurements;
        private IGenericRepository<MeasuringPoint> _measuringPoint;
        private IGenericRepository<ReadingStatus> _readingStatuses; 
        private IGenericRepository<WaterMeter> _waterMeters;
        private IGenericRepository<User> _user;

        public UnitOfWork(WmContext context)
        {
            _context = context;
        }

        //??= if variable on left is null then instanciate new objects
        //instanciating a public obj
        public IGenericRepository<Measurement> Measurements => _measurements ??= new GenericRepository<Measurement>(_context);
        public IGenericRepository<MeasuringPoint> MeasuringPoints => _measuringPoint ??= new GenericRepository<MeasuringPoint>(_context);
        public IGenericRepository<ReadingStatus> ReadingStatuses => _readingStatuses ??= new GenericRepository<ReadingStatus>(_context);
        public IGenericRepository<WaterMeter> WaterMeters => _waterMeters ??= new GenericRepository<WaterMeter>(_context);
        public IGenericRepository<User> User => _user ??= new GenericRepository<User>(_context);

        //method used to release the allocated resources in the context
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

       //saving all changes made to the context
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
