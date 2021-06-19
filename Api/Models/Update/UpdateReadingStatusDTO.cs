using Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Update
{
    public class UpdateReadingStatusDTO : CreateReadingStatusDTO
    {
        public IList<CreateMeasurementDTO> Measurements { get; set; }
    }
}
