using Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class MeasuringPointDTO : CreateMeasuringPointDTO
    {
       public int Id { get; set; }
       public RouteDTO Route { get; set; }
       public IList<WaterMeterDTO> WaterMeters { get; set; }
    }
}
