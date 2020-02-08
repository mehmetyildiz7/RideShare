using AdessoRideShare.Database.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Business
{
    public class UserService
    {
        private RideShareDbContext _rideShareDbContext;
        private IServiceProvider _provider;

        public UserService(RideShareDbContext rideShareDbContext, IServiceProvider provider)
        {
            _rideShareDbContext = rideShareDbContext;
            _provider = provider;
        }

        public async Task<User> CreateUserAsync()
        {
            var user = _rideShareDbContext.Users.Add(new User { });
            await _rideShareDbContext.SaveChangesAsync();
            return user.Entity;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _rideShareDbContext.FindAsync<User>(userId);
            _rideShareDbContext.Users.Remove(user);
            await _rideShareDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _rideShareDbContext.FindAsync<User>(userId);
        }

        public async Task<TravelPlan> PublishTravelPlanAsync(int userId, TravelPlan travelPlan)
        {
            TravelPlan _travelPlan;
            travelPlan.UserTravelPlans.Add(new UserTravelPlan
            {
                IsUserOwner = true,
                TravelPlan = travelPlan,
                UserId = userId,
            });

            using (var scope = _provider.CreateScope())
            {
                var travelPlanService = scope.ServiceProvider.GetService<TravelPlanService>();
                _travelPlan = await travelPlanService.CreateTravelPlanAsync(travelPlan);
            }

            return _travelPlan;
        }

        public bool AttendToTravelPlan(int userId, int travelPlanId)
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var travelPlanService = scope.ServiceProvider.GetService<TravelPlanService>();
                    var travelPlan = travelPlanService.GetTravelPlan(travelPlanId);

                    var attendedUsers = travelPlan.UserTravelPlans.Where(x => x.IsUserOwner == false).ToList();
                    int availableSeatCount = travelPlan.SeatCount - attendedUsers.Count;
                    if(availableSeatCount <= 0) // Check if there is an available seat exists
                    {
                        return false;
                    }
                    else
                    {
                        foreach (var user in attendedUsers)
                        {
                            if(user.UserId == userId)
                            {
                                return false; // If user already attended to travel plan return false
                            }
                        }

                        travelPlan.UserTravelPlans.Add(new UserTravelPlan // Add the user to the travel plan and return true
                        {
                            UserId = userId,
                            TravelPlan = travelPlan,
                            IsUserOwner = false,
                        });
                        _rideShareDbContext.SaveChanges();
                        return true;
                    }
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
