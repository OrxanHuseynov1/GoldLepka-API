using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class TranslationClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caption_ar",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Caption_en",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Caption_ru",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShopAdressText_ar",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShopAdressText_en",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShopAdressText_ru",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption_ar",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "Caption_en",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "Caption_ru",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "ShopAdressText_ar",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ShopAdressText_en",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ShopAdressText_ru",
                table: "Contacts");
        }
    }
}
