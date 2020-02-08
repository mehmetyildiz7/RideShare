using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdessoRideShare.Business;
using AdessoRideShare.Database.Models;
using AdessoRideShare.Web.Dtos;
using AdessoRideShare.Web.Inputs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace AdessoRideShare.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private IServiceProvider _provider;

        public UserController(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Returns the user object if found by given id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetUser")]
        [SwaggerResponse(200, Type = typeof(User))]
        [SwaggerResponse(404)]
        [Produces("application/json")]
        public IActionResult GetUser([FromQuery]GetUserInput input)
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetService<UserService>();
                    var user = userService.GetUser(input.UserId);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var dto = new GetUserDto();
                        dto.UserId = user.UserId;
                        foreach (var userTravelPlan in user.UserTravelPlans)
                        {
                            dto.TravelPlans.Add(new TravelPlanDto
                            {
                                Date = userTravelPlan.TravelPlan.Date,
                                DepartureCityId = userTravelPlan.TravelPlan.DepartureCityId,
                                Description = userTravelPlan.TravelPlan.Description,
                                DestinationCityId = userTravelPlan.TravelPlan.DestinationCityId,
                                IsActive = userTravelPlan.TravelPlan.IsActive,
                                SeatCount = userTravelPlan.TravelPlan.SeatCount,
                                TravelPlanId = userTravelPlan.TravelPlan.TravelPlanId,
                            });
                        }

                        return Ok(dto);
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("PublishTravelPlan")]
        public async Task<IActionResult> PublishTravelPlan(int userId, [FromBody]TravelPlan travelPlan)
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetService<UserService>();
                    var plan = await userService.PublishTravelPlanAsync(userId, travelPlan);
                    return Ok(plan);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
