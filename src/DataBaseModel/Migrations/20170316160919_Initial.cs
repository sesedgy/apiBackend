using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseModel.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abiturient",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                    UploadedFileName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    WhoGiveEducationDocument = table.Column<string>(nullable: true),
                    WhoUpdate = table.Column<Guid>(nullable: false),
                    СertificateOfMilitaryNumber = table.Column<string>(nullable: true),
                    СertificateOfMilitaryOffice = table.Column<string>(nullable: true),
                    СertificateOfMilitarySeries = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abiturient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FormOfEducation = table.Column<string>(nullable: true),
                    NameSpecialty = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    HashSalt = table.Column<string>(nullable: true),
                    IdClient = table.Column<Guid>(nullable: false),
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
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abiturient");

            migrationBuilder.DropTable(
                name: "Specialty");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
