using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson1.Models
{
    public class Price
    {
        [Key]
        public int PriceId { get; set; }

        [ForeignKey("Resort")]
        public int ResortId { get; set; }
        public virtual Resort Resort { get; set; }

        public decimal DAY1Hour { get; set; }

        public decimal DAY2Hours { get; set; }
        public decimal DAY3Hours { get; set; }
        public decimal DAY4Hours { get; set; }

        public decimal NIGHT1Hour { get; set; }
        public decimal NIGHT2Hours { get; set; }

        public decimal NIGHT3Hours { get; set; }
        public decimal NIGHT4Hours { get; set; }

    }

    public class PriceDto
    {
        
        public int PriceId { get; set; }       
        public int ResortId { get; set; }     
        public decimal DAY1Hour { get; set; }
        public decimal DAY2Hours { get; set; }
        public decimal DAY3Hours { get; set; }
        public decimal DAY4Hours { get; set; }
        public decimal NIGHT1Hour { get; set; }
        public decimal NIGHT2Hours { get; set; }
        public decimal NIGHT3Hours { get; set; }
        public decimal NIGHT4Hours { get; set; }

    }
}