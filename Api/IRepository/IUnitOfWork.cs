using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.IRepository
{
    //unit of work pattern
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Measurement> Measurements { get; }
        IGenericRepository<MeasuringPoint> MeasuringPoints { get; }
        IGenericRepository<ReadingStatus> ReadingStatuses { get; }
        IGenericRepository<Route> Routes { get; }
        IGenericRepository<WaterMeter> WaterMeters { get; }

        Task Save();
    }
}
