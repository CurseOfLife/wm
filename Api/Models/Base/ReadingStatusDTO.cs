using Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ReadingStatusDTO : CreateReadingStatusDTO
    {  
        public int Id { get; set; }
       // public IList<MeasurementDTO> Measurements { get; set; }
    }
}
