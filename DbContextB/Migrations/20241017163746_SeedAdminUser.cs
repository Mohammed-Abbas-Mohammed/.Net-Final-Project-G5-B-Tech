using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContextB.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecificationTranslations_ProductSpecifications_ProductSpecificationId",
                table: "ProductSpecificationTranslations");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecificationTranslations_ProductSpecificationId",
                table: "ProductSpecificationTranslations");

            migrationBuilder.DropColumn(
                name: "ProductSpecificationId",
                table: "ProductSpecificationTranslations");

            migrationBuilder.InsertData(
                schema: "security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "46b1ad51-48a1-4374-85e8-c7dc67afd054", 0, "Hamza St", "Sohag", "1e3097e5-4049-4f81-bd15-570f2d255ed9", "Egypt", "moh.alnoby216@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEJSQS92AP7MwS6jo5OwUuEcw0rUM7EzzOB9Jk8MwXbFzkCWalDOfzUjy27f3OOvDbQ==", null, false, "12345", "a0a324fb-ace2-4171-b063-44d9f97dddb5", false, "Mohammed Abbas", "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecificationTranslations_ProductSpecifications_SpecificationId",
                table: "ProductSpecificationTranslations",
                column: "SpecificationId",
                principalTable: "ProductSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecificationTranslations_ProductSpecifications_SpecificationId",
                table: "ProductSpecificationTranslations");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "46b1ad51-48a1-4374-85e8-c7dc67afd054");

            migrationBuilder.AddColumn<int>(
                name: "ProductSpecificationId",
                table: "ProductSpecificationTranslations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationTranslations_ProductSpecificationId",
                table: "ProductSpecificationTranslations",
                column: "ProductSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecificationTranslations_ProductSpecifications_ProductSpecificationId",
                table: "ProductSpecificationTranslations",
                column: "ProductSpecificationId",
                principalTable: "ProductSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
