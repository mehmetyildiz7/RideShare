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

        [HttpPut("CreateUser")]
        [SwaggerResponse(200, Type = typeof(User))]
        [SwaggerResponse(500)]
        [Produces("application/json")]
        public async Task<IActionResult> CreateUser()
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetService<UserService>();
                    return Ok(await userService.CreateUserAsync());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Returns the user object if found by given id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetUser")]
        [SwaggerResponse(200, Type = typeof(GetUserDto))]
        [SwaggerResponse(404)]
        [SwaggerResponse(500)]
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

        /// <summary>
        /// Creates Travel Plan for user with given values.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("PublishTravelPlan")]
        [SwaggerResponse(200, Type = typeof(PublishTravelPlanDto))]
        [SwaggerResponse(500)]
        [Produces("application/json")]
        public async Task<IActionResult> PublishTravelPlan([FromQuery]PublishTravelPlanInput input)
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetService<UserService>();
                    var travelPlan = new TravelPlan
                    {
                        Date = DateTime.Parse(input.Date),
                        DepartureCityId = input.DepartureCityId,
                        Description = input.Description,
                        DestinationCityId = input.DestinationCityId,
                        IsActive = true,
                        SeatCount = input.SeatCount,
                        UserTravelPlans = new List<UserTravelPlan>(),
                    };
                    var plan = await userService.PublishTravelPlanAsync(input.UserId, travelPlan);

                    var dto = new PublishTravelPlanDto();
                    if (plan == null)
                    {
                        dto.Status = false;
                        dto.Message = "Couldn't publish travel plan";
                    }
                    else
                    {
                        dto.Status = true;
                        dto.Message = "Travel plan has been published";
                    }

                    return Ok(dto);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Sets the IsActive property of travel plan
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("SetTravelPlanStatus")]
        [SwaggerResponse(200, Type = typeof(void))]
        [SwaggerResponse(500)]
        public async Task<IActionResult> SetTravelPlanStatus([FromQuery]SetTravelPlanStatusInput input)
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var travelPlanService = scope.ServiceProvider.GetService<TravelPlanService>();
                    await travelPlanService.SetTravelPlanStatusAsync(input.TravelPlanId, input.IsActive);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Tries to attend to given travel plan with given userId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("AttendToTravelPlan")]
        [SwaggerResponse(200, Type = typeof(AttendToTravelPlanDto))]
        [SwaggerResponse(400)]
        [SwaggerResponse(500)]
        [Produces("application/json")]
        public async Task<IActionResult> AttendToTravelPlan([FromQuery]AttendToTravelPlanInput input)
        {
            try
            {
                using (var scope = _provider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetService<UserService>();
                    var result = userService.AttendToTravelPlan(input.UserId, input.TravelPlanId);
                    var dto = new AttendToTravelPlanDto();
                    dto.Status = result;

                    if (result == AttendStatus.Successful)
                    {
                        dto.Message = "Successfully attended to travel plan.";
                        return Ok(dto);
                    }
                    else if (result == AttendStatus.NoSeatsAvailable)
                    {
                        dto.Message = "No seats available.";
                        return BadRequest(dto);
                    }
                    else if (result == AttendStatus.IsOwner)
                    {
                        dto.Message = "Attender is the owner of the travel plan";
                        return BadRequest(dto);
                    }
                    else if (result == AttendStatus.AlreadyAttended)
                    {
                        dto.Message = "Already attended.";
                        return BadRequest(dto);
                    }
                    else if (result == AttendStatus.UserDoesntExist)
                    {
                        dto.Message = "User doesn't exist";
                        return BadRequest(dto);
                    }
                    else
                    {
                        dto.Message = "Couldn't attend to travel plan.";
                        return BadRequest(dto);
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
