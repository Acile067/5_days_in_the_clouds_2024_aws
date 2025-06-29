using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _5_days_in_the_clouds_2024.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class matchModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Team1Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Team2Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WinningTeamId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
