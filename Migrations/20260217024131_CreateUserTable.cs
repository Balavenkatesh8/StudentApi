using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Role_Name",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "Role_Name");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
