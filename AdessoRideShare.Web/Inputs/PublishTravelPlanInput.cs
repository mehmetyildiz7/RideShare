using AdessoRideShare.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Inputs
{
    public class PublishTravelPlanInput
    {
        [DefaultValue(1)]
        public int UserId { get; set; }
        [DefaultValue(3)]
        public int DepartureCityId { get; set; }
        [DefaultValue(4)]
        public int DestinationCityId { get; set; }
        [DefaultValue("2020-03-21 13:26")]
        public string Date { get; set; }
        [DefaultValue("Test Description")]

        public string Description { get; set; }
        [DefaultValue(5)]
        public int SeatCount { get; set; }
    }
}
