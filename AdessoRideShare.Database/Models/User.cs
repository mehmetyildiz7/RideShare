using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Database.Models
{
    public class User
    {
        public int UserId { get; set; }
        public virtual ICollection<UserTravelPlan> UserTravelPlans { get; set; }
    }
}
