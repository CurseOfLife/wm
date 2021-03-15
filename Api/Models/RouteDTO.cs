using Api.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class RouteDTO : CreateRouteDTO
    {
        public IList<MeasuringPointDTO> MeasuringPoints { get; set; }
    }
}
