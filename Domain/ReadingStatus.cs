using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReadingStatus
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public IList<Measurement> Measurements { get; set; }
    }
}
