using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEntities;

namespace Domain
{
    
    public class Measurement : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(WaterMeter))]
        public int WaterMeterId { get; set; }
        public WaterMeter WaterMeter { get; set; }

        [Required]
        [ForeignKey(nameof(ReadingStatus))]
        public int ReadingStatusId { get; set;}
        public ReadingStatus ReadingStatus { get; set; }

        [Required]
        [MaxLength(4, ErrorMessage = "Measurement value too long")]
        public int Value { get; set; }
       
        [Required]
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }

    }
}

