using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Inputs
{
    public class GetTravelPlansInput
    {
        [DefaultValue(1)]
        public int DepartureCityId { get; set; }
        [DefaultValue(2)]
        public int DestinationCityId { get; set; }
    }
}
