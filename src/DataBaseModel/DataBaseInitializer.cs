using System;
using System.Linq;
using DataBaseModel.Models;

namespace DataBaseModel
{
    public class DataBaseInitializer
    {
        public static void Initialize(DataBaseContext context)
        {
            
            context.Database.EnsureCreated();

            if (context.TeachersTypesWork.Any())
            {
                return;   // DB has been seeded
            }

            var teachersTypesWorks = new TeachersTypesWork[]
            {
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Лекции", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Практические занятия", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Лабораторные работы", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Практические занятия на 2-ого преподавателя на группу", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Количество лабораторных работ", PercentLoad = "0.35", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Количество контрольных работ", PercentLoad = "0.35", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Количество расчетно-графических работ", PercentLoad = "0.35", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Количество типовых расчетов", PercentLoad = "0.35", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Количество рефератов", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Лабораторные работы, на 2-го преподавателя на группу", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Комплексные межкафедральные игры", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Учения", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Деловая игра -2 преподавателя  на группу", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Курсовая работа", PercentLoad = "3", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Курсовой проект", PercentLoad = "4", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new TeachersTypesWork() {TeachersTypesWorkId = Guid.NewGuid(), Name = "Прием экзаменов 6 часов на группу", PercentLoad = "1", CreatedDate= DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"}
            };
            foreach (var teachersTypesWork in teachersTypesWorks)
            {
                context.TeachersTypesWork.Add(teachersTypesWork);
            }

            var faculties = new Faculty[]
            {
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "ПБС", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Управления и экономики ГПС", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Информационых технологий УНК АСИИТ", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "СЭАСС УНК АСИИТ", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Надзорной деятельности (УНКД ОНД)", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Защиты населения и территорий (ЗНиТ) УНК ГЗ", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Гражданской защиты (ГЗ) УНК ГЗ", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Пожарной тактики и службы (ПТиС) УНК Пожаротушения", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Пожарно-строевой и газодымозащитной подготовки(ПСиГП) УНК Пожаротушения", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Пожарной техники (УНК \"ПиАСТ\")", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Кадрового, правового и психологического обеспечения (КПиПО)", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Пожарной безопасности и технологических процессов", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Процессов горения (УНК ПГиЭБ)", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Экологической безопасности (УНК ПГиЭБ)", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Пожарной автоматики", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Инженерной теплофизики и гидравлики", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Общей и специальной химии", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Физической подготовки и спорта", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Механики и инженерной графики", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Физики", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Высшей математики", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Истории и экономической теории", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Философии", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Иностранных языков", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"},
                new Faculty() {FacultyId = Guid.NewGuid(), Name = "Русского языка и культуры речи", CreatedDate = DateTime.Now, UpdatedDate = DateTime.MinValue, WhoUpdate = "auto"}
            };
            foreach (var faculty in faculties)
            {
                context.Faculty.Add(faculty);
            }

            var specialities = new Specialty[]
            {
                new Specialty(){SpecialtyId = new Guid(), FormOfEducation = "Очная", NameSpecialty = "Пожарная безопасность", Qualification = "Специалист"},
                new Specialty(){SpecialtyId = new Guid(), FormOfEducation = "Очная", NameSpecialty = "Техносферная безопасность", Qualification = "Бакалавр"},
            };
            foreach (var s in specialities)
            {
                context.Specialities.Add(s);
            }
            context.SaveChanges();

        }

    }
}
