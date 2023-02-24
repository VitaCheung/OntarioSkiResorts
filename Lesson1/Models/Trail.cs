using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;


namespace Lesson1.Models
{
    public class Trail
    {
        [Key]
        public int TrailId { get; set; }

        [ForeignKey("Resort")]
        public int ResortId { get; set; }
        public virtual Resort Resort { get; set; }

        public int BeginnerTrails { get; set; }
        public int IntermediateTrails { get; set; }
        public int AdvancedTrails { get; set; }
        public int TerrainPark { get; set; }
        public int TubingPark { get; set; }
       
    }

    public class TrailDto
    { 
        public int TrailId { get; set; }

        public int ResortId { get; set; }

        public string ResortName { get; set; }

        public int BeginnerTrails { get; set; }
        public int IntermediateTrails { get; set; }
        public int AdvancedTrails { get; set; }
        public int TerrainPark { get; set; }
        public int TubingPark { get; set; }
        
    }
}