using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Dtos
{
    public class TravelPlanDto
    {
        public int TravelPlanId { get; set; }
        public int DestinationCityId { get; set; }
        public int DepartureCityId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int SeatCount { get; set; }
        public bool IsActive { get; set; }
    }
}
