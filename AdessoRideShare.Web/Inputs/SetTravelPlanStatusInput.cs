using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Inputs
{
    public class SetTravelPlanStatusInput
    {
        public int TravelPlanId { get; set; }
        public bool IsActive { get; set; }
    }
}
