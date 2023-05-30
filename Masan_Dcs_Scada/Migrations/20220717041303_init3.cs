using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Masan_Dcs_Scada.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Line = table.Column<int>(nullable: false),
                    Machine = table.Column<int>(nullable: false),
                    Shift = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true),
                    StandardSpeed = table.Column<int>(nullable: false),
                    Time1 = table.Column<int>(nullable: false),
                    Time2 = table.Column<int>(nullable: false),
                    Time3 = table.Column<int>(nullable: false),
                    Time4 = table.Column<int>(nullable: false),
                    Time5 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemAttributes_Products_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Products",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemAttributes_ProductCode",
                table: "SystemAttributes",
                column: "ProductCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemAttributes");
        }
    }
}
