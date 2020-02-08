using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdessoRideShare.Business;
using AdessoRideShare.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                using(var scope = _provider.CreateScope())
                {
                    var userService = scope.ServiceProvider.GetService<UserService>();
                    var user = await userService.GetUserAsync(userId);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(user);
                    }
                }
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }

       
        [HttpPost("PublishTravelPlan")]
        public async Task<IActionResult> PublishTravelPlan(int userId, [FromBody]TravelPlan travelPlan)
        {
            try
            {
                using(var scope = _provider.CreateScope())
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
