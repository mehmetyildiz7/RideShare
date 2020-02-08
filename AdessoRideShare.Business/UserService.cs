using AdessoRideShare.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Business
{

    public enum AttendStatus
    {
        IsOwner,
        Successful,
        Failure,
        NoSeatsAvailable,
        AlreadyAttended,
        UserDoesntExist,
    }

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
            var user = await _rideShareDbContext.Users.AddAsync(new User { });
            await _rideShareDbContext.SaveChangesAsync();
            return user.Entity;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _rideShareDbContext.FindAsync<User>(userId);
            _rideShareDbContext.Users.Remove(user);
            await _rideShareDbContext.SaveChangesAsync();
        }

        public User GetUser(int userId)
        {
            return _rideShareDbContext.Users.Where(x => x.UserId == userId)
                .Include(user => user.UserTravelPlans)
                .ThenInclude(utp => utp.TravelPlan)
                .First();
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

        public AttendStatus AttendToTravelPlan(int userId, int travelPlanId)
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var travelPlanService = scope.ServiceProvider.GetService<TravelPlanService>();
                    var travelPlan = travelPlanService.GetTravelPlan(travelPlanId);

                    var attendedUsers = travelPlan.UserTravelPlans.ToList();
                    var ownerUser = attendedUsers.Where(x => x.IsUserOwner).First();
                    if(ownerUser.UserId == userId) // If the owner user tries to attend, don't attend
                    {
                        return AttendStatus.IsOwner;
                    }

                    attendedUsers.Remove(ownerUser); // Remove the owner user from list

                    int availableSeatCount = travelPlan.SeatCount - attendedUsers.Count;
                    if(availableSeatCount <= 0) // Check if there is an available seat exists
                    {
                        return AttendStatus.NoSeatsAvailable;
                    }
                    else
                    {
                        foreach (var user in attendedUsers)
                        {
                            if(user.UserId == userId)
                            {
                                return AttendStatus.AlreadyAttended; // If user already attended to travel plan return false
                            }
                        }

                        var attendingUser = GetUser(userId);
                        if(attendingUser == null)
                        {
                            return AttendStatus.UserDoesntExist; // if attending user doesn't exist return false
                        }
                        travelPlan.UserTravelPlans.Add(new UserTravelPlan // Add the user to the travel plan and return true
                        {
                            User = attendingUser,
                            TravelPlan = travelPlan,
                            IsUserOwner = false,
                        });
                        var result = _rideShareDbContext.Update(travelPlan);
                        _rideShareDbContext.SaveChanges();
                        return AttendStatus.Successful;
                    }
                }
            }
            catch(Exception e)
            {
                return AttendStatus.Failure;
            }
        }
    }
}
