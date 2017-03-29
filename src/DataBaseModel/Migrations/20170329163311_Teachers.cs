using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseModel.Migrations
{
    public partial class Teachers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedFileName",
                table: "Abiturient");

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Faculty = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    StatusDiscipline = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    WhoUpdate = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BuildingRegistration = table.Column<string>(nullable: true),
                    Citizenship = table.Column<string>(nullable: true),
                    CityRegistration = table.Column<string>(nullable: true),
                    CountryRegistration = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DateEducationDocument = table.Column<DateTime>(nullable: false),
                    DistrictRegistration = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Faculty = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    FlatRegistration = table.Column<string>(nullable: true),
                    HouseRegistration = table.Column<string>(nullable: true),
                    HousingRegistration = table.Column<string>(nullable: true),
                    INN = table.Column<string>(nullable: true),
                    IndexRegistration = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LocalityRegistration = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    NumberEducationDocument = table.Column<string>(nullable: true),
                    PassportDate = table.Column<DateTime>(nullable: false),
                    PassportIssueOrg = table.Column<string>(nullable: true),
                    PassportNumber = table.Column<string>(nullable: true),
                    PassportSeries = table.Column<string>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true),
                    RegionRegistration = table.Column<string>(nullable: true),
                    SNILS = table.Column<string>(nullable: true),
                    SalaryPerHour = table.Column<string>(nullable: true),
                    Scientist = table.Column<string>(nullable: true),
                    SeriesEducationDocument = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Speciality = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StreetRegistration = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    WhoGiveEducationDocument = table.Column<string>(nullable: true),
                    WhoUpdate = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeachersTypesWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PercentLoad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersTypesWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeachersWork",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Curs = table.Column<string>(nullable: true),
                    GroupId = table.Column<Guid>(nullable: false),
                    HoursWork = table.Column<string>(nullable: true),
                    Semester = table.Column<string>(nullable: true),
                    TeacherId = table.Column<Guid>(nullable: false),
                    TeachersTypeWorkId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersWork", x => x.Id);
                });

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Abiturient",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Abiturient");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "TeachersTypesWork");

            migrationBuilder.DropTable(
                name: "TeachersWork");

            migrationBuilder.AddColumn<string>(
                name: "UploadedFileName",
                table: "Abiturient",
                nullable: true);
        }
    }
}
