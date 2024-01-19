using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeTaskkMVC4.Migrations
{
    public partial class maintable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ExpertImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ExpertImages");
        }
    }
}
