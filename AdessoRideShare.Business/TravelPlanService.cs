using AdessoRideShare.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdessoRideShare.Business
{
    public class TravelPlanService
    {
        private RideShareDbContext _rideShareDbContext;

        public TravelPlanService(RideShareDbContext rideShareDbContext)
        {
            _rideShareDbContext = rideShareDbContext;
        }

        public async Task<TravelPlan> CreateTravelPlanAsync(TravelPlan travelPlan)
        {
            try
            {
                var plan = await _rideShareDbContext.AddAsync(travelPlan);
                await _rideShareDbContext.SaveChangesAsync();
                return plan.Entity;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public TravelPlan GetTravelPlan(int travelPlanId)
        {
            var plan = _rideShareDbContext.TravelPlans.Where(tp => tp.TravelPlanId == travelPlanId).Include("UserTravelPlans").First();
            return plan;
        }

        public async Task SetTravelPlanStatusAsync(int planId, bool isActive)
        {
            var plan = await _rideShareDbContext.FindAsync<TravelPlan>(planId);
            plan.IsActive = isActive;
            await _rideShareDbContext.SaveChangesAsync();
        }

        public List<TravelPlan> FindTravelPlans(int destinationCityId, int departureCityId)
        {
            var plans = _rideShareDbContext.TravelPlans
                .Where(tp => tp.DestinationCityId == destinationCityId && tp.DepartureCityId == departureCityId)
                .Include("UserTravelPlans").ToList();

            return plans;
        }
    }
}
