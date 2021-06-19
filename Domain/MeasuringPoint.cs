using Domain.Identity;
using IEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    //note desc can be null, so can route and measurement
    //change
    public class MeasuringPoint : IEntity
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

        [JsonProperty("watermeters")]
        public IList<WaterMeter> WaterMeters { get; set; }

        [ForeignKey("UserId")]

        public string UserId { get; set; }

        [IgnoreDataMember]
        public User User { get; set; }

    }
}
