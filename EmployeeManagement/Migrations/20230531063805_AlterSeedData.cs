using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class AlterSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "id", "Department", "Email", "Name" },
                values: new object[,]
                {
                    { 2, 4, "mou@gmail.com", "Moshumee Mollika Moue" },
                    { 3, 2, "Rihan@gmail.com", "Ibrahim Rihan Ayat" },
                    { 4, 1, "hena@gmail.com", "Hena" },
                    { 5, 1, "safia@gmail.com", "Safia" },
                    { 6, 4, "safin@gmail.com", "Safin" },
                    { 7, 4, "ayan@gmail.com", "Ayan" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "id", "Department", "Email", "Name" },
                values: new object[] { 1, 2, "rana123123@gmail.com", "Reazul Islam Rana" });
        }
    }
}
