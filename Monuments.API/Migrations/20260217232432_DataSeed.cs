using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Monuments.API.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Cities",
                newName: "Slogan");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "Slogan" },
                values: new object[,]
                {
                    { 1, "Abia", "God's Own State" },
                    { 2, "Bauchi", "Pearl of Tourism" },
                    { 3, "Lagos", "Center of Excellence" }
                });

            migrationBuilder.InsertData(
                table: "Monuments",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "A UNESCO World Heritage site featuring the Palace of the Hidi.", "Sukur Cultural Landscape" },
                    { 2, 1, "Ancient earthworks and one of the world's largest man-made earthworks.", "Benin City Walls and Moat" },
                    { 3, 2, "Nigeria's first-storey building built in 1845.", "First Storey Building" },
                    { 4, 2, "A UNESCO World Heritage site featuring the Palace of the Hidi.", "Sukur Cultural Landscape" },
                    { 5, 3, "Historic walls and the palace of the Emir.", "Ancient Kano City Walls & Gidan Makama" },
                    { 6, 3, "Exhibits relics from the Civil War and Nigerian military history.", "National War Museum" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Monuments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Monuments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Monuments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Monuments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Monuments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Monuments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "Slogan",
                table: "Cities",
                newName: "Description");
        }
    }
}
