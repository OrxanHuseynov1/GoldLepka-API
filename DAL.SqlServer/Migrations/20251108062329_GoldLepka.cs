using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class GoldLepka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopAdressText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatsappNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstegramIsActive = table.Column<bool>(type: "bit", nullable: false),
                    InstegramAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TikTokIsActive = table.Column<bool>(type: "bit", nullable: false),
                    TikTokAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacebookIsActive = table.Column<bool>(type: "bit", nullable: false),
                    FacebookAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
