using AdessoRideShare.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdessoRideShare.Web
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {

        }

        public override void SetupDatabase(IServiceCollection services)
        {
            services.AddDbContext<RideShareDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });
        }
    }
}
