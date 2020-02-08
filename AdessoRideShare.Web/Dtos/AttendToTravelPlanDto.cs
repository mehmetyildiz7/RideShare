using AdessoRideShare.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Dtos
{
    public class AttendToTravelPlanDto
    {
        public AttendStatus Status { get; set; }
        public string Message { get; set; }
    }
}
