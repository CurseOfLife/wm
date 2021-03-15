using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class MeasurementDTO : CreateMeasurementDTO
    {
        public int Id { get; set; }

        public WaterMeterDTO WaterMeter { get; set; }
        public ReadingStatusDTO ReadingStatus { get; set; }

    }
}
