﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson1.Models.ViewModels
{
    public class DetailsPrice
    {
        public PriceDto SelectedPrice { get; set; }
        public IEnumerable<ResortDto> RelatedResort { get; set; }
    }
}