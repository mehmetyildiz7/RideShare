using AdessoRideShare.Web.Inputs;
using FluentAssertions;
using System;
using System.Net;
using Xunit;

namespace AdessoRideShare.IntegrationTests
{
    public class UserControllerTests : IntegrationTest
    {
        [Fact]
        public async void GetUsers_ReturnsNotFound_WithoutAnyUsers()
        {
            // Act
            var response = await TestClient.GetAsync("/User/GetUser?userId=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void PublishTravelPlan_ReturnsBadRequest_WithoutAnyUsers()
        {
            // Arrange
            var input = new PublishTravelPlanInput
            {
                Date = DateTime.Now.ToString(),
                DepartureCityId = 1,
                Description = "Test",
                DestinationCityId = 2,
                SeatCount = 5,
                UserId = 1
            };

            string queryString = "Date={0}&DepartureCityId={1}&Description={2}&DestinationCityId={3}&SeatCount={4}&UserId={5}";
            queryString = String.Format(queryString, input.Date, input.DepartureCityId, input.Description, input.DestinationCityId, input.SeatCount, input.UserId);

            // Act
            var response = await TestClient.PostAsync("/User/PublishTravelPlan?" + queryString, null); // body is null 

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        [Fact]
        public async void SetTravelPlanStatus_ReturnsBadRequest_WithoutAnyTravelPlans()
        {
            // Arrange
            var input = new SetTravelPlanStatusInput
            {
                IsActive = false,
                TravelPlanId = 1,
            };

            string queryString = "IsActive={0}&TravelPlanId={1}";
            queryString = String.Format(queryString, input.IsActive, input.TravelPlanId);

            // Act
            var response = await TestClient.PostAsync("/User/SetTravelPlanStatus?" + queryString, null); // body is null

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void AttendToTravelPlan_ReturnsBadRequest_WithoutAnyUsersAndTravelPlans()
        {
            // Arrange
            var input = new AttendToTravelPlanInput
            {
                TravelPlanId = 1,
                UserId = 1,
            };

            string queryString = "TravelPlanId={0}&UserId={1}";
            queryString = String.Format(queryString, input.TravelPlanId, input.UserId);

            // Act
            var response = await TestClient.PostAsync("/User/AttendToTravelPlan?" + queryString, null); // body is null

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
