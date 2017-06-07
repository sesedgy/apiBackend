using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataBaseModel;

namespace DataBaseModel.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataBaseModel.Models.Abiturient", b =>
                {
                    b.Property<Guid>("AbiturientId")
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

                    b.Property<string>("FirstNameDP");

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

                    b.Property<string>("LastNameDP");

                    b.Property<string>("LastNameFather");

                    b.Property<string>("LastNameMother");

                    b.Property<string>("LegalAddress");

                    b.Property<string>("LevelEducation");

                    b.Property<string>("LocalityCustomer");

                    b.Property<string>("LocalityLive");

                    b.Property<string>("LocalityRegistration");

                    b.Property<string>("MiddleName");

                    b.Property<string>("MiddleNameCustomer");

                    b.Property<string>("MiddleNameDP");

                    b.Property<string>("MiddleNameFather");

                    b.Property<string>("MiddleNameMother");

                    b.Property<string>("MilitaryTicketCodeVus");

                    b.Property<string>("MilitaryTicketMilitaryOffice");

                    b.Property<string>("MilitaryTicketNumber");

                    b.Property<string>("MilitaryTicketRank");

                    b.Property<string>("MilitaryTicketSeries");

                    b.Property<DateTime>("MilitaryTicketWhenGive");

                    b.Property<string>("MilitaryTicketWhoGive");

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

                    b.Property<string>("PhotoPath");

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

                    b.Property<Guid?>("UserId")
                        .IsRequired();

                    b.Property<string>("WhoGiveEducationDocument");

                    b.Property<string>("WhoUpdate");

                    b.Property<string>("СertificateOfMilitaryNumber");

                    b.Property<string>("СertificateOfMilitaryOffice");

                    b.Property<string>("СertificateOfMilitarySeries");

                    b.HasKey("AbiturientId");

                    b.HasIndex("UserId");

                    b.ToTable("Abiturient");
                });

            modelBuilder.Entity("DataBaseModel.Models.Department", b =>
                {
                    b.Property<Guid>("DepartmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("DataBaseModel.Models.Discipline", b =>
                {
                    b.Property<Guid>("DisciplineId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid?>("FacultyId")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.Property<string>("StatusDiscipline");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("DisciplineId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Discipline");
                });

            modelBuilder.Entity("DataBaseModel.Models.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginDate");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("BuildingRegistration");

                    b.Property<string>("Citizenship");

                    b.Property<string>("CityRegistration");

                    b.Property<string>("CountryRegistration");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("DateEducationDocument");

                    b.Property<Guid?>("DepartmentId")
                        .IsRequired();

                    b.Property<string>("DistrictRegistration");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("FlatRegistration");

                    b.Property<string>("HouseRegistration");

                    b.Property<string>("HousingRegistration");

                    b.Property<string>("INN");

                    b.Property<string>("IndexRegistration");

                    b.Property<string>("LastName");

                    b.Property<string>("LocalityRegistration");

                    b.Property<string>("MiddleName");

                    b.Property<string>("MobilePhone");

                    b.Property<string>("NumberEducationDocument");

                    b.Property<DateTime>("PassportDate");

                    b.Property<string>("PassportIssueOrg");

                    b.Property<string>("PassportNumber");

                    b.Property<string>("PassportSeries");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("Position");

                    b.Property<string>("RegionRegistration");

                    b.Property<string>("SNILS");

                    b.Property<string>("SalaryPerHour");

                    b.Property<string>("SeriesEducationDocument");

                    b.Property<string>("Sex");

                    b.Property<string>("Status");

                    b.Property<string>("StreetRegistration");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<Guid?>("UserId")
                        .IsRequired();

                    b.Property<string>("WhoGiveEducationDocument");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("DataBaseModel.Models.Faculty", b =>
                {
                    b.Property<Guid>("FacultyId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("FacultyId");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("DataBaseModel.Models.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Begin");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Semester");

                    b.Property<Guid?>("SpecialtyId");

                    b.Property<string>("Status");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("GroupId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("DataBaseModel.Models.Specialty", b =>
                {
                    b.Property<Guid>("SpecialtyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CodeSpecialty");

                    b.Property<string>("FormOfEducation");

                    b.Property<string>("NameSpecialty");

                    b.Property<string>("Qualification");

                    b.HasKey("SpecialtyId");

                    b.ToTable("Specialty");
                });

            modelBuilder.Entity("DataBaseModel.Models.Student", b =>
                {
                    b.Property<Guid>("StudentId")
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

                    b.Property<DateTime>("DateBegin");

                    b.Property<DateTime>("DateEducationDocument");

                    b.Property<DateTime>("DateEnd");

                    b.Property<string>("DisciplineFive");

                    b.Property<string>("DisciplineFour");

                    b.Property<string>("DisciplineMinus");

                    b.Property<string>("DisciplineOne");

                    b.Property<string>("DisciplinePlus");

                    b.Property<string>("DisciplineThree");

                    b.Property<string>("DisciplineTwo");

                    b.Property<string>("DistrictCustomer");

                    b.Property<string>("DistrictLive");

                    b.Property<string>("DistrictRegistration");

                    b.Property<string>("FioRepresentative");

                    b.Property<string>("FirstName");

                    b.Property<string>("FirstNameCustomer");

                    b.Property<string>("FirstNameDP");

                    b.Property<string>("FirstNameFather");

                    b.Property<string>("FirstNameMother");

                    b.Property<string>("FlatCustomer");

                    b.Property<string>("FlatLive");

                    b.Property<string>("FlatRegistration");

                    b.Property<Guid?>("GroupId")
                        .IsRequired();

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

                    b.Property<bool>("IsStarosta");

                    b.Property<string>("Kpp");

                    b.Property<string>("LastName");

                    b.Property<string>("LastNameCustomer");

                    b.Property<string>("LastNameDP");

                    b.Property<string>("LastNameFather");

                    b.Property<string>("LastNameMother");

                    b.Property<string>("LastNameOld");

                    b.Property<string>("LegalAddress");

                    b.Property<string>("LevelEducation");

                    b.Property<string>("LocalityCustomer");

                    b.Property<string>("LocalityLive");

                    b.Property<string>("LocalityRegistration");

                    b.Property<string>("MiddleName");

                    b.Property<string>("MiddleNameCustomer");

                    b.Property<string>("MiddleNameDP");

                    b.Property<string>("MiddleNameFather");

                    b.Property<string>("MiddleNameMother");

                    b.Property<string>("MilitaryTicketCodeVus");

                    b.Property<string>("MilitaryTicketMilitaryOffice");

                    b.Property<string>("MilitaryTicketNumber");

                    b.Property<string>("MilitaryTicketRank");

                    b.Property<string>("MilitaryTicketSeries");

                    b.Property<DateTime>("MilitaryTicketWhenGive");

                    b.Property<string>("MilitaryTicketWhoGive");

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

                    b.Property<string>("PhotoDiplomPath");

                    b.Property<string>("PhotoPassport1Path");

                    b.Property<string>("PhotoPassport2Path");

                    b.Property<string>("PhotoPassportCustomer1Path");

                    b.Property<string>("PhotoPassportCustomer2Path");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("PhotoСertificateOfMilitary1Path");

                    b.Property<string>("PhotoСertificateOfMilitary2Path");

                    b.Property<string>("RegionCustomer");

                    b.Property<string>("RegionLive");

                    b.Property<string>("RegionRegistration");

                    b.Property<string>("SeriesEducationDocument");

                    b.Property<string>("Sex");

                    b.Property<string>("Status");

                    b.Property<string>("StreetCustomer");

                    b.Property<string>("StreetLive");

                    b.Property<string>("StreetRegistration");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<Guid?>("UserId")
                        .IsRequired();

                    b.Property<string>("WhoGiveEducationDocument");

                    b.Property<string>("WhoUpdate");

                    b.Property<string>("СertificateOfMilitaryNumber");

                    b.Property<string>("СertificateOfMilitaryOffice");

                    b.Property<string>("СertificateOfMilitarySeries");

                    b.HasKey("StudentId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("DataBaseModel.Models.Teacher", b =>
                {
                    b.Property<Guid>("TeacherId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AcademicDegree");

                    b.Property<string>("AcademicTitle");

                    b.Property<DateTime>("BeginDate");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("BuildingRegistration");

                    b.Property<string>("Citizenship");

                    b.Property<string>("CityRegistration");

                    b.Property<string>("CountryRegistration");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("DateEducationDocument");

                    b.Property<string>("DistrictRegistration");

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid?>("FacultyId")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("FlatRegistration");

                    b.Property<string>("HouseRegistration");

                    b.Property<string>("HousingRegistration");

                    b.Property<string>("INN");

                    b.Property<string>("IndexRegistration");

                    b.Property<string>("LastName");

                    b.Property<string>("LocalityRegistration");

                    b.Property<string>("MiddleName");

                    b.Property<string>("MobilePhone");

                    b.Property<string>("NumberEducationDocument");

                    b.Property<DateTime>("PassportDate");

                    b.Property<string>("PassportIssueOrg");

                    b.Property<string>("PassportNumber");

                    b.Property<string>("PassportSeries");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("RegionRegistration");

                    b.Property<string>("SNILS");

                    b.Property<string>("SalaryPerHour");

                    b.Property<string>("SeriesEducationDocument");

                    b.Property<string>("Sex");

                    b.Property<string>("Speciality");

                    b.Property<string>("Status");

                    b.Property<string>("StreetRegistration");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<Guid?>("UserId")
                        .IsRequired();

                    b.Property<string>("WhoGiveEducationDocument");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("TeacherId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("UserId");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("DataBaseModel.Models.TeachersTypesWork", b =>
                {
                    b.Property<Guid>("TeachersTypesWorkId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Name");

                    b.Property<string>("PercentLoad");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("TeachersTypesWorkId");

                    b.ToTable("TeachersTypesWork");
                });

            modelBuilder.Entity("DataBaseModel.Models.TeachersWork", b =>
                {
                    b.Property<Guid>("TeachersWorkId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Curs");

                    b.Property<Guid?>("GroupId");

                    b.Property<string>("HoursWork");

                    b.Property<string>("Semester");

                    b.Property<Guid?>("TeacherId");

                    b.Property<Guid?>("TeachersTypesWorkId");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("TeachersWorkId");

                    b.HasIndex("GroupId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("TeachersTypesWorkId");

                    b.ToTable("TeachersWork");
                });

            modelBuilder.Entity("DataBaseModel.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("HashSalt");

                    b.Property<bool>("IsOnline");

                    b.Property<DateTime>("LastActivityDate");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("WhoUpdate");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DataBaseModel.Models.Abiturient", b =>
                {
                    b.HasOne("DataBaseModel.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataBaseModel.Models.Discipline", b =>
                {
                    b.HasOne("DataBaseModel.Models.Faculty", "Faculty")
                        .WithMany("Disciplines")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataBaseModel.Models.Employee", b =>
                {
                    b.HasOne("DataBaseModel.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataBaseModel.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataBaseModel.Models.Group", b =>
                {
                    b.HasOne("DataBaseModel.Models.Specialty", "Specialty")
                        .WithMany("Groups")
                        .HasForeignKey("SpecialtyId");
                });

            modelBuilder.Entity("DataBaseModel.Models.Student", b =>
                {
                    b.HasOne("DataBaseModel.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataBaseModel.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataBaseModel.Models.Teacher", b =>
                {
                    b.HasOne("DataBaseModel.Models.Faculty", "Faculty")
                        .WithMany("Teachers")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataBaseModel.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataBaseModel.Models.TeachersWork", b =>
                {
                    b.HasOne("DataBaseModel.Models.Group", "Group")
                        .WithMany("TeachersWorks")
                        .HasForeignKey("GroupId");

                    b.HasOne("DataBaseModel.Models.Teacher", "Teacher")
                        .WithMany("TeachersWorks")
                        .HasForeignKey("TeacherId");

                    b.HasOne("DataBaseModel.Models.TeachersTypesWork", "TeachersTypesWork")
                        .WithMany("TeachersWorks")
                        .HasForeignKey("TeachersTypesWorkId");
                });
        }
    }
}
