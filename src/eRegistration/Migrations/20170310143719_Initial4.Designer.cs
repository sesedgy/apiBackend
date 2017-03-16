using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eRegistration.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20170310143719_Initial4")]
    partial class Initial4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("finalapp.Models.Abiturient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActualAddress");

                    b.Property<string>("BicOfTheBank");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("BuildingCustomer");

                    b.Property<string>("BuildingLive");

                    b.Property<string>("BuildingRegistration");

                    b.Property<string>("CheckingAccount");

                    b.Property<string>("Citizenship");

                    b.Property<string>("CityCustomer");

                    b.Property<string>("CityLive");

                    b.Property<string>("CityRegistration");

                    b.Property<string>("CorrespondingAccount");

                    b.Property<string>("CountryCustomer");

                    b.Property<string>("CountryLive");

                    b.Property<string>("CountryRegistration");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("DateEducationDocument");

                    b.Property<string>("DistrictCustomer");

                    b.Property<string>("DistrictLive");

                    b.Property<string>("DistrictRegistration");

                    b.Property<string>("FioRepresentative");

                    b.Property<string>("FirstName");

                    b.Property<string>("FirstNameCustomer");

                    b.Property<string>("FirstNameFather");

                    b.Property<string>("FirstNameMother");

                    b.Property<string>("FlatCustomer");

                    b.Property<string>("FlatLive");

                    b.Property<string>("FlatRegistration");

                    b.Property<string>("FormOfEducation");

                    b.Property<string>("HouseCustomer");

                    b.Property<string>("HouseLive");

                    b.Property<string>("HouseRegistration");

                    b.Property<string>("HousingCustomer");

                    b.Property<string>("HousingLive");

                    b.Property<string>("HousingRegistration");

                    b.Property<string>("IndexLive");

                    b.Property<string>("IndexRegistration");

                    b.Property<string>("Inn");

                    b.Property<string>("InnOfTheBank");

                    b.Property<string>("Kpp");

                    b.Property<string>("LastName");

                    b.Property<string>("LastNameCustomer");

                    b.Property<string>("LastNameFather");

                    b.Property<string>("LastNameMother");

                    b.Property<string>("LegalAddress");

                    b.Property<string>("LevelEducation");

                    b.Property<string>("LocalityCustomer");

                    b.Property<string>("LocalityLive");

                    b.Property<string>("LocalityRegistration");

                    b.Property<string>("MiddleName");

                    b.Property<string>("MiddleNameCustomer");

                    b.Property<string>("MiddleNameFather");

                    b.Property<string>("MiddleNameMother");

                    b.Property<string>("MobilePhone");

                    b.Property<string>("MobilePhoneCustomer");

                    b.Property<string>("MobilePhoneFather");

                    b.Property<string>("MobilePhoneMother");

                    b.Property<string>("NameOfTheBank");

                    b.Property<string>("NameOfTheOrganization");

                    b.Property<string>("Nation");

                    b.Property<string>("NumberEducationDocument");

                    b.Property<DateTime>("PassportDate");

                    b.Property<DateTime>("PassportDateCustomer");

                    b.Property<string>("PassportIssueOrg");

                    b.Property<string>("PassportIssueOrgCustomer");

                    b.Property<string>("PassportNumber");

                    b.Property<string>("PassportNumberCustomer");

                    b.Property<string>("PassportSeries");

                    b.Property<string>("PassportSeriesCustomer");

                    b.Property<string>("PaymentForm");

                    b.Property<string>("PhoneOfTheOrganisation");

                    b.Property<string>("Qualification");

                    b.Property<string>("RegionCustomer");

                    b.Property<string>("RegionLive");

                    b.Property<string>("RegionRegistration");

                    b.Property<string>("SeriesEducationDocument");

                    b.Property<string>("Sex");

                    b.Property<string>("Specialty");

                    b.Property<string>("StreetCustomer");

                    b.Property<string>("StreetLive");

                    b.Property<string>("StreetRegistration");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UploadedFileName");

                    b.Property<Guid>("UserId");

                    b.Property<string>("WhoGiveEducationDocument");

                    b.Property<Guid>("WhoUpdate");

                    b.HasKey("Id");

                    b.ToTable("Abiturient");
                });

            modelBuilder.Entity("finalapp.Models.Specialty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FormOfEducation");

                    b.Property<string>("NameSpecialty");

                    b.Property<string>("Qualification");

                    b.HasKey("Id");

                    b.ToTable("Specialty");
                });

            modelBuilder.Entity("finalapp.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("HashSalt");

                    b.Property<Guid>("IdClient");

                    b.Property<bool>("IsOnline");

                    b.Property<DateTime>("LastActivityDate");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
        }
    }
}
