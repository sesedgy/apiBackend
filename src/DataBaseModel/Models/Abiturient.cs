using System;

namespace DataBaseModel.Models
{
    public class Abiturient
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastNameDP { get; set; }
        public string FirstNameDP { get; set; }
        public string MiddleNameDP { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobilePhone { get; set; }
        public string Citizenship { get; set; }
        public string Sex { get; set; }
        public string Nation { get; set; }
        public string Specialty { get; set; }
        public string FormOfEducation { get; set; }
        public string Qualification { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportDate { get; set; }
        public string PassportIssueOrg { get; set; }
        public string CountryRegistration { get; set; }
        public string CityRegistration { get; set; }
        public string RegionRegistration { get; set; }
        public string DistrictRegistration { get; set; }
        public string LocalityRegistration { get; set; }
        public string StreetRegistration { get; set; }
        public string HouseRegistration { get; set; }
        public string BuildingRegistration { get; set; }
        public string HousingRegistration { get; set; }
        public string FlatRegistration { get; set; }
        public string IndexRegistration { get; set; }
        public string CountryLive { get; set; }
        public string CityLive { get; set; }
        public string RegionLive { get; set; }
        public string DistrictLive { get; set; }
        public string LocalityLive { get; set; }
        public string StreetLive { get; set; }
        public string HouseLive { get; set; }
        public string BuildingLive { get; set; }
        public string HousingLive { get; set; }
        public string FlatLive { get; set; }
        public string IndexLive { get; set; }
        public string LastNameMother { get; set; }
        public string FirstNameMother { get; set; }
        public string MiddleNameMother { get; set; }
        public string MobilePhoneMother { get; set; }
        public string LastNameFather { get; set; }
        public string FirstNameFather { get; set; }
        public string MiddleNameFather { get; set; }
        public string MobilePhoneFather { get; set; }
        public string LevelEducation { get; set; }
        public string SeriesEducationDocument { get; set; }
        public string NumberEducationDocument { get; set; }
        public DateTime DateEducationDocument { get; set; }
        public string WhoGiveEducationDocument { get; set; }
        public string UploadedFileName { get; set; }
        public string PaymentForm { get; set; }
        public string LastNameCustomer { get; set; }
        public string FirstNameCustomer { get; set; }
        public string MiddleNameCustomer { get; set; }
        public string MobilePhoneCustomer { get; set; }
        public string PassportSeriesCustomer { get; set; }
        public string PassportNumberCustomer { get; set; }
        public DateTime PassportDateCustomer { get; set; }
        public string PassportIssueOrgCustomer { get; set; }
        public string CountryCustomer { get; set; }
        public string CityCustomer { get; set; }
        public string RegionCustomer { get; set; }
        public string DistrictCustomer { get; set; }
        public string LocalityCustomer { get; set; }
        public string StreetCustomer { get; set; }
        public string HouseCustomer { get; set; }
        public string BuildingCustomer { get; set; }
        public string HousingCustomer { get; set; }
        public string FlatCustomer { get; set; }
        public string NameOfTheOrganization { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string LegalAddress { get; set; }
        public string ActualAddress { get; set; }
        public string NameOfTheBank { get; set; }
        public string InnOfTheBank { get; set; }
        public string BicOfTheBank { get; set; }
        public string CheckingAccount { get; set; }
        public string CorrespondingAccount { get; set; }
        public string FioRepresentative { get; set; }
        public string PhoneOfTheOrganisation { get; set; }
        public string СertificateOfMilitarySeries { get; set; }
        public string СertificateOfMilitaryNumber { get; set; }
        public string СertificateOfMilitaryOffice { get; set; }    //Где стоит на учете
        public string MilitaryTicketSeries { get; set; }
        public string MilitaryTicketNumber { get; set; }
        public DateTime MilitaryTicketWhenGive { get; set; }
        public string MilitaryTicketWhoGive { get; set; }
        public string MilitaryTicketRank { get; set; }
        public string MilitaryTicketCodeVus { get; set; }
        public string MilitaryTicketMilitaryOffice { get; set; }    //Где стоит на учете

        public Guid WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
