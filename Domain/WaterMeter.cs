using IEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class WaterMeter : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Code { get; set; }

        [MaxLength(4)]
        public int MaxValue { get; set; } //pronadji max na vodomeru, not null
        [MaxLength(4)]
        public int? StartingValue { get; set; }  //if null default 0
        public bool IsActive { get; set; }

        [ForeignKey(nameof(MeasuringPoint))]
        public int MeasuringPointId { get; set; }
        public MeasuringPoint MeasuringPoint { get; set; }

        public IList<Measurement> Measurements { get; set; }
    }
}
