using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApi.Migrations
{
    /// <inheritdoc />
    public partial class FixAdminPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                schema: "master",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Created_Timestamp",
                schema: "master",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "master",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Last_Modified_Timestamp",
                schema: "master",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Countries",
                schema: "master",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "StudentEducations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Student_Education_Id",
                table: "StudentEducations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Student_Id",
                table: "StudentEducations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "StudentEducations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Year_Of_Passing",
                table: "StudentEducations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentDocument_Id",
                table: "StudentDocuments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Student_Education_Id",
                table: "StudentDocuments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Student_Id",
                table: "StudentDocuments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Last_Modified_By",
                table: "Country",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Country_Id");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Admin_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Admin_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "StudentEducations");

            migrationBuilder.DropColumn(
                name: "Student_Education_Id",
                table: "StudentEducations");

            migrationBuilder.DropColumn(
                name: "Student_Id",
                table: "StudentEducations");

            migrationBuilder.DropColumn(
                name: "University",
                table: "StudentEducations");

            migrationBuilder.DropColumn(
                name: "Year_Of_Passing",
                table: "StudentEducations");

            migrationBuilder.DropColumn(
                name: "StudentDocument_Id",
                table: "StudentDocuments");

            migrationBuilder.DropColumn(
                name: "Student_Education_Id",
                table: "StudentDocuments");

            migrationBuilder.DropColumn(
                name: "Student_Id",
                table: "StudentDocuments");

            migrationBuilder.DropColumn(
                name: "Last_Modified_By",
                table: "Country");

            migrationBuilder.EnsureSchema(
                name: "master");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries",
                newSchema: "master");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_Timestamp",
                schema: "master",
                table: "Countries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "master",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Last_Modified_Timestamp",
                schema: "master",
                table: "Countries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                schema: "master",
                table: "Countries",
                column: "Country_Id");
        }
    }
}
