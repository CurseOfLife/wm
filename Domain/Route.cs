using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    //dont forget to add to dtos the changes after identity
    public class Route
    {
        [Key]
        public int Id { get; set; }

        // userID
        //userID who created the route
        //day it was created

        public IList<MeasuringPoint> MeasuringPoints { get; set; }
    }
}
