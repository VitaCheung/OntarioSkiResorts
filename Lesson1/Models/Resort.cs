using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson1.Models
{
    public class Resort
    {
        [Key]
        public int ResortId { get; set; }   
        public string ResortName { get; set; }  
        public string address { get; set; } 
        public string description { get; set; } 
        public string NumberOfTrailsOpen { get; set; }  
        public string Contact { get; set; }    
        public bool WheelchairAccessible { get; set; }  
        public int NumberOfRestaurants { get; set; }    

        public string Link { get; set; }



    }
    public class ResortDto
    {
        public int ResortId { get; set; }
        public string ResortName { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string NumberOfTrailsOpen { get; set; }
        public string Contact { get; set; }
        public bool WheelchairAccessible { get; set; }
        public int NumberOfRestaurants { get; set; }

        public string Link { get; set; }
    }
}