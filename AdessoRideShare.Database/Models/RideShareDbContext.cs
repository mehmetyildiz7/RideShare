using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Database.Models
{
    public class RideShareDbContext : DbContext
    {
        public RideShareDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TravelPlan> TravelPlans { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTravelPlan>()
                .HasKey(utp => new { utp.UserId, utp.TravelPlanId });
            modelBuilder.Entity<UserTravelPlan>()
                .HasOne(utp => utp.User)
                .WithMany(u => u.UserTravelPlans)
                .HasForeignKey(utp => utp.UserId);
            modelBuilder.Entity<UserTravelPlan>()
                .HasOne(utp => utp.TravelPlan)
                .WithMany(tp => tp.UserTravelPlans)
                .HasForeignKey(utp => utp.TravelPlanId);

            List<City> cities = new List<City>();

            int totalHeight = 500;
            int totalWidth = 1000;
            int squareArea = 50;

            int cityId = 1;
            for (int y = 0; y < totalHeight / squareArea; y++)
            {
                for (int x = 0; x < totalWidth / squareArea; x++)
                {
                    cities.Add(new City
                    {
                        CityId = cityId,
                        XCoord = x * squareArea,
                        YCoord = y * squareArea,
                    });
                    cityId++;
                }
            }

            modelBuilder.Entity<City>().HasData(cities);

            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
            });

        }
    }
}
