using Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Update
{
    public class UpdateWaterMeterDTO : CreateWaterMeterDTO
    {
        public CreateMeasuringPointDTO MeasuringPoint { get; set; }

        public IList<CreateMeasurementDTO> Measurements { get; set; }
    }
}
