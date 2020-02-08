using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Database.Models
{
    public class TravelPlan
    {
        public int TravelPlanId { get; set; }
        public int DestinationCityId { get; set; }
        public int DepartureCityId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int SeatCount { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UserTravelPlan> UserTravelPlans { get; set; }
    }
}
