using Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class MeasuringPointDTO : CreateMeasuringPointDTO
    {
        //could inherit the updatemeasuring point as it just has ilist right now
        //instead of inheriting the creatempointdto
        //but if update.. has specific fields we couldnt
       public int Id { get; set; }

       
        public IList<WaterMeterDTO> WaterMeters { get; set; }
    }
}
