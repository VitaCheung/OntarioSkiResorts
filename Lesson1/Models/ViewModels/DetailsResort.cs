using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson1.Models.ViewModels
{
    public class DetailsResort
    {
        public ResortDto SelectedResort { get; set; }

        public IEnumerable<TrailDto> RelatedTrail { get; set; }    

    }
}