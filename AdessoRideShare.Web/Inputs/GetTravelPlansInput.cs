using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Inputs
{
    public class GetTravelPlansInput
    {
        public int DepartureCityId { get; set; }
        public int DestinationCityId { get; set; }
    }
}
