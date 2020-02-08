using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Inputs
{
    public class AttendToTravelPlanInput
    {
        [DefaultValue(1)]
        public int TravelPlanId { get; set; }
        [DefaultValue(2)]
        public int UserId { get; set; }
    }
}
