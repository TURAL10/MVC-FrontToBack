using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeTaskkMVC4.Migrations
{
    public partial class sayslider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaySliderContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaySliderContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaySliderImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    SaySliderContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaySliderImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaySliderImages_SaySliderContents_SaySliderContentId",
                        column: x => x.SaySliderContentId,
                        principalTable: "SaySliderContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaySliderImages_SaySliderContentId",
                table: "SaySliderImages",
                column: "SaySliderContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaySliderImages");

            migrationBuilder.DropTable(
                name: "SaySliderContents");
        }
    }
}
