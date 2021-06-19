using Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class WaterMeterDTO : CreateWaterMeterDTO
    {
        
        public int Id { get; set; }  
        public MeasuringPointDTO MeasuringPoint { get; set; }

      //  public IList<MeasurementDTO> Measurements { get; set; }
    }
}
