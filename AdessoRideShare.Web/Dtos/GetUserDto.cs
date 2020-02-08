﻿using AdessoRideShare.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Web.Dtos
{
    public class GetUserDto
    {
        public int UserId { get; set; }
        public List<TravelPlanDto> TravelPlans { get; set; } = new List<TravelPlanDto>();
    }
}
