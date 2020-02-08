using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdessoRideShare.Database.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XCoord = table.Column<int>(nullable: false),
                    YCoord = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "TravelPlans",
                columns: table => new
                {
                    TravelPlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationCityId = table.Column<int>(nullable: false),
                    DepartureCityId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SeatCount = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPlans", x => x.TravelPlanId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserTravelPlan",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TravelPlanId = table.Column<int>(nullable: false),
                    IsUserOwner = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTravelPlan", x => new { x.UserId, x.TravelPlanId });
                    table.ForeignKey(
                        name: "FK_UserTravelPlan_TravelPlans_TravelPlanId",
                        column: x => x.TravelPlanId,
                        principalTable: "TravelPlans",
                        principalColumn: "TravelPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTravelPlan_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "XCoord", "YCoord" },
                values: new object[,]
                {
                    { 1, 0, 0 },
                    { 129, 400, 300 },
                    { 130, 450, 300 },
                    { 131, 500, 300 },
                    { 132, 550, 300 },
                    { 133, 600, 300 },
                    { 134, 650, 300 },
                    { 135, 700, 300 },
                    { 136, 750, 300 },
                    { 137, 800, 300 },
                    { 138, 850, 300 },
                    { 139, 900, 300 },
                    { 140, 950, 300 },
                    { 141, 0, 350 },
                    { 142, 50, 350 },
                    { 143, 100, 350 },
                    { 144, 150, 350 },
                    { 145, 200, 350 },
                    { 146, 250, 350 },
                    { 147, 300, 350 },
                    { 148, 350, 350 },
                    { 149, 400, 350 },
                    { 128, 350, 300 },
                    { 150, 450, 350 },
                    { 127, 300, 300 },
                    { 125, 200, 300 },
                    { 104, 150, 250 },
                    { 105, 200, 250 },
                    { 106, 250, 250 },
                    { 107, 300, 250 },
                    { 108, 350, 250 },
                    { 109, 400, 250 },
                    { 110, 450, 250 },
                    { 111, 500, 250 },
                    { 112, 550, 250 },
                    { 113, 600, 250 },
                    { 114, 650, 250 },
                    { 115, 700, 250 },
                    { 116, 750, 250 },
                    { 117, 800, 250 },
                    { 118, 850, 250 },
                    { 119, 900, 250 },
                    { 120, 950, 250 },
                    { 121, 0, 300 },
                    { 122, 50, 300 },
                    { 123, 100, 300 },
                    { 124, 150, 300 },
                    { 126, 250, 300 },
                    { 151, 500, 350 },
                    { 152, 550, 350 },
                    { 153, 600, 350 },
                    { 180, 950, 400 },
                    { 181, 0, 450 },
                    { 182, 50, 450 },
                    { 183, 100, 450 },
                    { 184, 150, 450 },
                    { 185, 200, 450 },
                    { 186, 250, 450 },
                    { 187, 300, 450 },
                    { 188, 350, 450 },
                    { 189, 400, 450 },
                    { 190, 450, 450 },
                    { 191, 500, 450 },
                    { 192, 550, 450 },
                    { 193, 600, 450 },
                    { 194, 650, 450 },
                    { 195, 700, 450 },
                    { 196, 750, 450 },
                    { 197, 800, 450 },
                    { 198, 850, 450 },
                    { 199, 900, 450 },
                    { 200, 950, 450 },
                    { 179, 900, 400 },
                    { 178, 850, 400 },
                    { 177, 800, 400 },
                    { 176, 750, 400 },
                    { 154, 650, 350 },
                    { 155, 700, 350 },
                    { 156, 750, 350 },
                    { 157, 800, 350 },
                    { 158, 850, 350 },
                    { 159, 900, 350 },
                    { 160, 950, 350 },
                    { 161, 0, 400 },
                    { 162, 50, 400 },
                    { 163, 100, 400 },
                    { 103, 100, 250 },
                    { 164, 150, 400 },
                    { 166, 250, 400 },
                    { 167, 300, 400 },
                    { 168, 350, 400 },
                    { 169, 400, 400 },
                    { 170, 450, 400 },
                    { 171, 500, 400 },
                    { 172, 550, 400 },
                    { 173, 600, 400 },
                    { 174, 650, 400 },
                    { 175, 700, 400 },
                    { 165, 200, 400 },
                    { 102, 50, 250 },
                    { 101, 0, 250 },
                    { 100, 950, 200 },
                    { 27, 300, 50 },
                    { 28, 350, 50 },
                    { 29, 400, 50 },
                    { 30, 450, 50 },
                    { 31, 500, 50 },
                    { 32, 550, 50 },
                    { 33, 600, 50 },
                    { 34, 650, 50 },
                    { 35, 700, 50 },
                    { 36, 750, 50 },
                    { 37, 800, 50 },
                    { 38, 850, 50 },
                    { 39, 900, 50 },
                    { 40, 950, 50 },
                    { 41, 0, 100 },
                    { 42, 50, 100 },
                    { 43, 100, 100 },
                    { 44, 150, 100 },
                    { 45, 200, 100 },
                    { 46, 250, 100 },
                    { 47, 300, 100 },
                    { 26, 250, 50 },
                    { 48, 350, 100 },
                    { 25, 200, 50 },
                    { 23, 100, 50 },
                    { 2, 50, 0 },
                    { 3, 100, 0 },
                    { 4, 150, 0 },
                    { 5, 200, 0 },
                    { 6, 250, 0 },
                    { 7, 300, 0 },
                    { 8, 350, 0 },
                    { 9, 400, 0 },
                    { 10, 450, 0 },
                    { 11, 500, 0 },
                    { 12, 550, 0 },
                    { 13, 600, 0 },
                    { 14, 650, 0 },
                    { 15, 700, 0 },
                    { 16, 750, 0 },
                    { 17, 800, 0 },
                    { 18, 850, 0 },
                    { 19, 900, 0 },
                    { 20, 950, 0 },
                    { 21, 0, 50 },
                    { 22, 50, 50 },
                    { 24, 150, 50 },
                    { 49, 400, 100 },
                    { 50, 450, 100 },
                    { 76, 750, 150 },
                    { 77, 800, 150 },
                    { 78, 850, 150 },
                    { 79, 900, 150 },
                    { 80, 950, 150 },
                    { 81, 0, 200 },
                    { 82, 50, 200 },
                    { 83, 100, 200 },
                    { 84, 150, 200 },
                    { 85, 200, 200 },
                    { 86, 250, 200 },
                    { 87, 300, 200 },
                    { 88, 350, 200 },
                    { 89, 400, 200 },
                    { 90, 450, 200 },
                    { 91, 500, 200 },
                    { 92, 550, 200 },
                    { 93, 600, 200 },
                    { 94, 650, 200 },
                    { 95, 700, 200 },
                    { 96, 750, 200 },
                    { 97, 800, 200 },
                    { 51, 500, 100 },
                    { 98, 850, 200 },
                    { 75, 700, 150 },
                    { 73, 600, 150 },
                    { 52, 550, 100 },
                    { 53, 600, 100 },
                    { 54, 650, 100 },
                    { 55, 700, 100 },
                    { 56, 750, 100 },
                    { 57, 800, 100 },
                    { 58, 850, 100 },
                    { 59, 900, 100 },
                    { 60, 950, 100 },
                    { 61, 0, 150 },
                    { 62, 50, 150 },
                    { 63, 100, 150 },
                    { 64, 150, 150 },
                    { 65, 200, 150 },
                    { 66, 250, 150 },
                    { 67, 300, 150 },
                    { 68, 350, 150 },
                    { 69, 400, 150 },
                    { 70, 450, 150 },
                    { 71, 500, 150 },
                    { 72, 550, 150 },
                    { 74, 650, 150 },
                    { 99, 900, 200 }
                });

            migrationBuilder.InsertData(
                table: "TravelPlans",
                columns: new[] { "TravelPlanId", "Date", "DepartureCityId", "Description", "DestinationCityId", "IsActive", "SeatCount" },
                values: new object[] { 1, new DateTime(2020, 2, 8, 15, 16, 55, 379, DateTimeKind.Local).AddTicks(1908), 1, "Migration Travel Plan", 2, true, 5 });

            migrationBuilder.InsertData(
                table: "Users",
                column: "UserId",
                value: 1);

            migrationBuilder.InsertData(
                table: "UserTravelPlan",
                columns: new[] { "UserId", "TravelPlanId", "IsUserOwner" },
                values: new object[] { 1, 1, true });

            migrationBuilder.CreateIndex(
                name: "IX_UserTravelPlan_TravelPlanId",
                table: "UserTravelPlan",
                column: "TravelPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "UserTravelPlan");

            migrationBuilder.DropTable(
                name: "TravelPlans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
