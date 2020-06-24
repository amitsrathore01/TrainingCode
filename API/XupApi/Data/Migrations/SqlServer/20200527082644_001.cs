using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XupApi.Data.Migrations.SqlServer
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckRegister",
                columns: table => new
                {
                    CheckId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FrequencyType = table.Column<string>(nullable: false),
                    Frequency = table.Column<int>(nullable: false),
                    IsScheduled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckRegister", x => x.CheckId);
                });

            migrationBuilder.CreateTable(
                name: "CheckRun",
                columns: table => new
                {
                    CheckRunId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    RunTime = table.Column<string>(nullable: true),
                    LastRunOn = table.Column<DateTime>(nullable: false),
                    CheckRegisterCheckId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckRun", x => x.CheckRunId);
                    table.ForeignKey(
                        name: "FK_CheckRun_CheckRegister_CheckRegisterCheckId",
                        column: x => x.CheckRegisterCheckId,
                        principalTable: "CheckRegister",
                        principalColumn: "CheckId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckRun_CheckRegisterCheckId",
                table: "CheckRun",
                column: "CheckRegisterCheckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckRun");

            migrationBuilder.DropTable(
                name: "CheckRegister");
        }
    }
}
