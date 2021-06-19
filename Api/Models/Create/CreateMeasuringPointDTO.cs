using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Create
{
    public class CreateMeasuringPointDTO
    {

        //add required where needed
        [MaxLength(95)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string Number { get; set; }

        [MaxLength(35)]
        public string Place { get; set; } //city, village
        public string Description { get; set; } //should be owner 
     
 
       
    }
}
