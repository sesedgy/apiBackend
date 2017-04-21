﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseModel.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        [Required]
        public virtual User User { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobilePhone { get; set; }
        public string Citizenship { get; set; }
        public string Sex { get; set; }
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

        public string INN { get; set; }
        public string SNILS { get; set; }
        public string SeriesEducationDocument { get; set; }
        public string NumberEducationDocument { get; set; }
        public DateTime DateEducationDocument { get; set; }
        public string WhoGiveEducationDocument { get; set; }

        public virtual Department Department { get; set; }                  //Отдел
        public string Position { get; set; }                        //Должность
        public DateTime BeginDate { get; set; }                     //Дата начала работы
        public DateTime EndDate { get; set; }

        public string SalaryPerHour { get; set; }                   //Почасовая ставка
        public string PhotoPath { get; set; }
        public string Status { get; set; }

        public string WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
