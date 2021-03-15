using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class CreateMeasurementDTO
    {  
        [Required]
        public int WaterMeterId { get; set; }
       
        [Required]
        public int ReadingStatusId { get; set; }        

        [Required]
        [MaxLength(4, ErrorMessage = "Measurement value too long")]
        public int Value { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
    }
}
