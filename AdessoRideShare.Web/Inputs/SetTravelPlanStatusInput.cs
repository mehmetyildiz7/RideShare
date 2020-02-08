using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Inputs
{
    public class SetTravelPlanStatusInput
    {
        [DefaultValue(1)]
        public int TravelPlanId { get; set; }
        [DefaultValue(false)]
        public bool IsActive { get; set; }
    }
}
