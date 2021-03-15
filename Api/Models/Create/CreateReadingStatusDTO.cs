using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Create
{
    public class CreateReadingStatusDTO
    {
        [MaxLength(64)]
        public string Name { get; set; }
      
    }
}
