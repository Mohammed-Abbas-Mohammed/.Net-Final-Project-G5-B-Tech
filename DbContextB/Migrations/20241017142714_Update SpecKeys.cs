using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContextB.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSpecKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "SpecificationKeys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationKeys_LanguageId",
                table: "SpecificationKeys",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationKeys_Languages_LanguageId",
                table: "SpecificationKeys",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationKeys_Languages_LanguageId",
                table: "SpecificationKeys");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationKeys_LanguageId",
                table: "SpecificationKeys");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "SpecificationKeys");
        }
    }
}
