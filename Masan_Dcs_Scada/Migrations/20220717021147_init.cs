using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Masan_Dcs_Scada.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "HeadShifts",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadShifts", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    HeadShiftCode1 = table.Column<string>(nullable: true),
                    ShiftStartTime1 = table.Column<DateTime>(nullable: false),
                    HeadShiftCode2 = table.Column<string>(nullable: true),
                    ShiftStartTime2 = table.Column<DateTime>(nullable: false),
                    HeadShiftCode3 = table.Column<string>(nullable: true),
                    ShiftStartTime3 = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shifts_HeadShifts_HeadShiftCode1",
                        column: x => x.HeadShiftCode1,
                        principalTable: "HeadShifts",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shifts_HeadShifts_HeadShiftCode2",
                        column: x => x.HeadShiftCode2,
                        principalTable: "HeadShifts",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shifts_HeadShifts_HeadShiftCode3",
                        column: x => x.HeadShiftCode3,
                        principalTable: "HeadShifts",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_HeadShiftCode1",
                table: "Shifts",
                column: "HeadShiftCode1");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_HeadShiftCode2",
                table: "Shifts",
                column: "HeadShiftCode2");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_HeadShiftCode3",
                table: "Shifts",
                column: "HeadShiftCode3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "HeadShifts");
        }
    }
}
