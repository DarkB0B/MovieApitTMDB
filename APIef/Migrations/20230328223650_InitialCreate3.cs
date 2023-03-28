using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIef.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "MoviesCollections",
                newName: "MovieCollections");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "MovieCollections",
                newName: "MoviesCollections");
        }
    }
}
