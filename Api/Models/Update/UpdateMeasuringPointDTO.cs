using Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Update
{
    public class UpdateMeasuringPointDTO : CreateMeasuringPointDTO
    {
        //check if this has to be taken out

        public IList<CreateWaterMeterDTO> WaterMeters { get; set; }
    }
}
