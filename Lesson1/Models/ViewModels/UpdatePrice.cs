using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson1.Models.ViewModels
{
    public class UpdatePrice
    {
        public PriceDto SelectedPrice { get; set; }

        public IEnumerable<ResortDto> ResortOptions { get; set; }
        //public ResortDto RelatedResort { get; set; }
    }
}