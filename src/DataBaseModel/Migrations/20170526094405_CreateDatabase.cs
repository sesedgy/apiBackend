using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseModel.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    FacultyId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.FacultyId);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    SpecialtyId = table.Column<Guid>(nullable: false),
                    CodeSpecialty = table.Column<string>(nullable: true),
                    FormOfEducation = table.Column<string>(nullable: true),
                    NameSpecialty = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.SpecialtyId);
                });

            migrationBuilder.CreateTable(
                name: "TeachersTypesWork",
                columns: table => new
                {
                    TeachersTypesWorkId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PercentLoad = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersTypesWork", x => x.TeachersTypesWorkId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    HashSalt = table.Column<string>(nullable: true),
                    IsOnline = table.Column<bool>(nullable: false),
                    LastActivityDate = table.Column<DateTime>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    DisciplineId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    FacultyId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    StatusDiscipline = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.DisciplineId);
                    table.ForeignKey(
                        name: "FK_Discipline_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(nullable: false),
                    Begin = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Semester = table.Column<string>(nullable: true),
                    SpecialtyId = table.Column<Guid>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_Specialty_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialty",
                        principalColumn: "SpecialtyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Abiturient",
                columns: table => new
                {
                    AbiturientId = table.Column<Guid>(nullable: false),
                    ActualAddress = table.Column<string>(nullable: true),
                    BicOfTheBank = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BuildingCustomer = table.Column<string>(nullable: true),
                    BuildingLive = table.Column<string>(nullable: true),
                    BuildingRegistration = table.Column<string>(nullable: true),
                    CheckingAccount = table.Column<string>(nullable: true),
                    Citizenship = table.Column<string>(nullable: true),
                    CityCustomer = table.Column<string>(nullable: true),
                    CityLive = table.Column<string>(nullable: true),
                    CityRegistration = table.Column<string>(nullable: true),
                    CorrespondingAccount = table.Column<string>(nullable: true),
                    CountryCustomer = table.Column<string>(nullable: true),
                    CountryLive = table.Column<string>(nullable: true),
                    CountryRegistration = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DateEducationDocument = table.Column<DateTime>(nullable: false),
                    DistrictCustomer = table.Column<string>(nullable: true),
                    DistrictLive = table.Column<string>(nullable: true),
                    DistrictRegistration = table.Column<string>(nullable: true),
                    FioRepresentative = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    FirstNameCustomer = table.Column<string>(nullable: true),
                    FirstNameDP = table.Column<string>(nullable: true),
                    FirstNameFather = table.Column<string>(nullable: true),
                    FirstNameMother = table.Column<string>(nullable: true),
                    FlatCustomer = table.Column<string>(nullable: true),
                    FlatLive = table.Column<string>(nullable: true),
                    FlatRegistration = table.Column<string>(nullable: true),
                    FormOfEducation = table.Column<string>(nullable: true),
                    HouseCustomer = table.Column<string>(nullable: true),
                    HouseLive = table.Column<string>(nullable: true),
                    HouseRegistration = table.Column<string>(nullable: true),
                    HousingCustomer = table.Column<string>(nullable: true),
                    HousingLive = table.Column<string>(nullable: true),
                    HousingRegistration = table.Column<string>(nullable: true),
                    IndexLive = table.Column<string>(nullable: true),
                    IndexRegistration = table.Column<string>(nullable: true),
                    Inn = table.Column<string>(nullable: true),
                    InnOfTheBank = table.Column<string>(nullable: true),
                    Kpp = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LastNameCustomer = table.Column<string>(nullable: true),
                    LastNameDP = table.Column<string>(nullable: true),
                    LastNameFather = table.Column<string>(nullable: true),
                    LastNameMother = table.Column<string>(nullable: true),
                    LegalAddress = table.Column<string>(nullable: true),
                    LevelEducation = table.Column<string>(nullable: true),
                    LocalityCustomer = table.Column<string>(nullable: true),
                    LocalityLive = table.Column<string>(nullable: true),
                    LocalityRegistration = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    MiddleNameCustomer = table.Column<string>(nullable: true),
                    MiddleNameDP = table.Column<string>(nullable: true),
                    MiddleNameFather = table.Column<string>(nullable: true),
                    MiddleNameMother = table.Column<string>(nullable: true),
                    MilitaryTicketCodeVus = table.Column<string>(nullable: true),
                    MilitaryTicketMilitaryOffice = table.Column<string>(nullable: true),
                    MilitaryTicketNumber = table.Column<string>(nullable: true),
                    MilitaryTicketRank = table.Column<string>(nullable: true),
                    MilitaryTicketSeries = table.Column<string>(nullable: true),
                    MilitaryTicketWhenGive = table.Column<DateTime>(nullable: false),
                    MilitaryTicketWhoGive = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    MobilePhoneCustomer = table.Column<string>(nullable: true),
                    MobilePhoneFather = table.Column<string>(nullable: true),
                    MobilePhoneMother = table.Column<string>(nullable: true),
                    NameOfTheBank = table.Column<string>(nullable: true),
                    NameOfTheOrganization = table.Column<string>(nullable: true),
                    Nation = table.Column<string>(nullable: true),
                    NumberEducationDocument = table.Column<string>(nullable: true),
                    PassportDate = table.Column<DateTime>(nullable: false),
                    PassportDateCustomer = table.Column<DateTime>(nullable: false),
                    PassportIssueOrg = table.Column<string>(nullable: true),
                    PassportIssueOrgCustomer = table.Column<string>(nullable: true),
                    PassportNumber = table.Column<string>(nullable: true),
                    PassportNumberCustomer = table.Column<string>(nullable: true),
                    PassportSeries = table.Column<string>(nullable: true),
                    PassportSeriesCustomer = table.Column<string>(nullable: true),
                    PaymentForm = table.Column<string>(nullable: true),
                    PhoneOfTheOrganisation = table.Column<string>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    RegionCustomer = table.Column<string>(nullable: true),
                    RegionLive = table.Column<string>(nullable: true),
                    RegionRegistration = table.Column<string>(nullable: true),
                    SeriesEducationDocument = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Specialty = table.Column<string>(nullable: true),
                    StreetCustomer = table.Column<string>(nullable: true),
                    StreetLive = table.Column<string>(nullable: true),
                    StreetRegistration = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    WhoGiveEducationDocument = table.Column<string>(nullable: true),
                    WhoUpdate = table.Column<string>(nullable: true),
                    СertificateOfMilitaryNumber = table.Column<string>(nullable: true),
                    СertificateOfMilitaryOffice = table.Column<string>(nullable: true),
                    СertificateOfMilitarySeries = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abiturient", x => x.AbiturientId);
                    table.ForeignKey(
                        name: "FK_Abiturient_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    BuildingRegistration = table.Column<string>(nullable: true),
                    Citizenship = table.Column<string>(nullable: true),
                    CityRegistration = table.Column<string>(nullable: true),
                    CountryRegistration = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DateEducationDocument = table.Column<DateTime>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    DistrictRegistration = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
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
                    Position = table.Column<string>(nullable: true),
                    RegionRegistration = table.Column<string>(nullable: true),
                    SNILS = table.Column<string>(nullable: true),
                    SalaryPerHour = table.Column<string>(nullable: true),
                    SeriesEducationDocument = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StreetRegistration = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    WhoGiveEducationDocument = table.Column<string>(nullable: true),
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    TeacherId = table.Column<Guid>(nullable: false),
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
                    FacultyId = table.Column<Guid>(nullable: true),
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
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Teacher_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teacher_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Student_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachersWork",
                columns: table => new
                {
                    TeachersWorkId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Curs = table.Column<string>(nullable: true),
                    GroupId = table.Column<Guid>(nullable: true),
                    HoursWork = table.Column<string>(nullable: true),
                    Semester = table.Column<string>(nullable: true),
                    TeacherId = table.Column<Guid>(nullable: true),
                    TeachersTypesWorkId = table.Column<Guid>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    WhoUpdate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersWork", x => x.TeachersWorkId);
                    table.ForeignKey(
                        name: "FK_TeachersWork_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachersWork_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachersWork_TeachersTypesWork_TeachersTypesWorkId",
                        column: x => x.TeachersTypesWorkId,
                        principalTable: "TeachersTypesWork",
                        principalColumn: "TeachersTypesWorkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abiturient_UserId",
                table: "Abiturient",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_FacultyId",
                table: "Discipline",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserId",
                table: "Employee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_SpecialtyId",
                table: "Group",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_GroupId",
                table: "Student",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_UserId",
                table: "Student",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_FacultyId",
                table: "Teacher",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_UserId",
                table: "Teacher",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersWork_GroupId",
                table: "TeachersWork",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersWork_TeacherId",
                table: "TeachersWork",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersWork_TeachersTypesWorkId",
                table: "TeachersWork",
                column: "TeachersTypesWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abiturient");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "TeachersWork");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "TeachersTypesWork");

            migrationBuilder.DropTable(
                name: "Specialty");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
