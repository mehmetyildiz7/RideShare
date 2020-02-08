using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Database.Models
{
    public class UserTravelPlan
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TravelPlanId { get; set; }
        public TravelPlan TravelPlan { get; set; }
        public bool IsUserOwner { get; set; }
    }
}
