using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    //note desc can be null, so can route and measurement
    //change
    public class MeasuringPoint
    {
       
        [Key]
        public int Id { get; set; }

        [MaxLength(95)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string Number { get; set; }

        [MaxLength(35)]
        public string Place { get; set; } //city, village
        public string Description { get; set; } //should be owner 

        [ForeignKey(nameof(Route))]
        public int? RouteId { get; set; }
        public Route Route { get; set; }
        public IList<WaterMeter> WaterMeters { get; set; }

    }
}
