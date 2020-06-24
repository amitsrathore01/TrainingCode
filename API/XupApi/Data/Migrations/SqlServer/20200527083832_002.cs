using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XupApi.Data.Migrations.SqlServer
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckRun_CheckRegister_CheckRegisterCheckId",
                table: "CheckRun");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckRun",
                table: "CheckRun");

            migrationBuilder.DropIndex(
                name: "IX_CheckRun_CheckRegisterCheckId",
                table: "CheckRun");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckRegister",
                table: "CheckRegister");

            migrationBuilder.DropColumn(
                name: "CheckRunId",
                table: "CheckRun");

            migrationBuilder.DropColumn(
                name: "CheckRegisterCheckId",
                table: "CheckRun");

            migrationBuilder.DropColumn(
                name: "CheckId",
                table: "CheckRegister");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CheckRun",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CheckId",
                table: "CheckRun",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CheckRegister",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckRun",
                table: "CheckRun",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckRegister",
                table: "CheckRegister",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CheckRun_CheckId",
                table: "CheckRun",
                column: "CheckId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckRun_CheckRegister_CheckId",
                table: "CheckRun",
                column: "CheckId",
                principalTable: "CheckRegister",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckRun_CheckRegister_CheckId",
                table: "CheckRun");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckRun",
                table: "CheckRun");

            migrationBuilder.DropIndex(
                name: "IX_CheckRun_CheckId",
                table: "CheckRun");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckRegister",
                table: "CheckRegister");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CheckRun");

            migrationBuilder.DropColumn(
                name: "CheckId",
                table: "CheckRun");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CheckRegister");

            migrationBuilder.AddColumn<Guid>(
                name: "CheckRunId",
                table: "CheckRun",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CheckRegisterCheckId",
                table: "CheckRun",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CheckId",
                table: "CheckRegister",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckRun",
                table: "CheckRun",
                column: "CheckRunId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckRegister",
                table: "CheckRegister",
                column: "CheckId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckRun_CheckRegisterCheckId",
                table: "CheckRun",
                column: "CheckRegisterCheckId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckRun_CheckRegister_CheckRegisterCheckId",
                table: "CheckRun",
                column: "CheckRegisterCheckId",
                principalTable: "CheckRegister",
                principalColumn: "CheckId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
