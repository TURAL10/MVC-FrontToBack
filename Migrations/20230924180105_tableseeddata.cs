using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeTaskkMVC4.Migrations
{
    public partial class tableseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53528be9-e851-4547-bc65-f5dfabdcbce9", "2b214b4c-83c8-4dfe-88a7-b4b9da2f5f6e", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b5d5f56e-5f18-4c97-b3a3-e828ef5e1352", "b89cb96c-edaf-465f-9c43-47da517237db", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da0b56e9-708a-422e-87f1-56789cbcaa30", "1448bbd4-bfbe-4810-9297-6e9afe129c04", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53528be9-e851-4547-bc65-f5dfabdcbce9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d5f56e-5f18-4c97-b3a3-e828ef5e1352");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da0b56e9-708a-422e-87f1-56789cbcaa30");
        }
    }
}
