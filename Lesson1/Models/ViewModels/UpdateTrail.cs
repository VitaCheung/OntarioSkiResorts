using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson1.Models.ViewModels
{
    public class UpdateTrail
    {
        public TrailDto SelectedTrail { get; set; }

        public IEnumerable<ResortDto> ResortOptions { get; set; }
    }
}