using Api.IRepository;
using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WmContext _context;

        private IGenericRepository<Measurement> _measurements;
        private IGenericRepository<MeasuringPoint> _measuringPoint;
        private IGenericRepository<ReadingStatus> _readingStatuses;
        private IGenericRepository<Route> _routes;
        private IGenericRepository<WaterMeter> _waterMeters;

        public UnitOfWork(WmContext context)
        {
            _context = context;
        }

        public IGenericRepository<Measurement> Measurements => _measurements ??= new GenericRepository<Measurement>(_context);

        public IGenericRepository<MeasuringPoint> MeasuringPoints => _measuringPoint ??= new GenericRepository<MeasuringPoint>(_context);

        public IGenericRepository<ReadingStatus> ReadingStatuses => _readingStatuses ??= new GenericRepository<ReadingStatus>(_context);

        public IGenericRepository<Route> Routes => _routes ??= new GenericRepository<Route>(_context);

        public IGenericRepository<WaterMeter> WaterMeters => _waterMeters ??= new GenericRepository<WaterMeter>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

     
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
