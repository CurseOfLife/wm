using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Create
{
    public class CreateWaterMeterDTO
    {
        [Required]
        [MaxLength(64)]
        public string Code { get; set; }

        [MaxLength(4)]
        public int MaxValue { get; set; } //pronadji max na vodomeru, not null
        [MaxLength(4)]
        public int? StartingValue { get; set; }  //if null default 0
        public bool IsActive { get; set; }
      
        public int MeasuringPointId { get; set; }
       
    }
}
