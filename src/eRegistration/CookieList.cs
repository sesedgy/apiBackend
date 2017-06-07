using System;
using System.Collections.Generic;
using DataBaseModel;
using DataBaseModel.Models;
using eRegistration.Controllers;
using Microsoft.AspNetCore.Http;

namespace eRegistration
{
    public class CookieList
    {

        private static CookieList _instance;
        private readonly Dictionary<Guid, Cookie> _dictionary;

        private CookieList()
        {
            _dictionary = new Dictionary<Guid, Cookie>();
        }

        public static CookieList GetInstance()
        {
            if (_instance == null)
                _instance = new CookieList();
            return _instance;
        }

        /// <summary>
        /// Добавить куки в список активных.
        /// </summary>
        /// <param name="user">Необходимый пользователь</param>
        /// <returns>[(string)Id куки, (string)Роль]</returns>        
        public string[] AddCookie(User user)
        {
            foreach (var item in _dictionary)
            {
                if (item.Value.User.UserId == user.UserId)
                {
                    _dictionary.Remove(item.Key);
                    break;
                }
            }
            var guid = Guid.NewGuid();
            var cookie = new Cookie() {CookieId = guid, User = user};
            switch (user.Role)
            {
                case "IsAbiturient":
                    cookie.IsAbiturient = true;
                    break;
                case "IsStudent":
                    cookie.IsStudent = true;
                    break;
                case "IsStudentLeader":
                    cookie.IsStudentLeader = true;
                    break;
                case "IsTeacher":
                    cookie.IsTeacher = true;
                    break;
                case "IsWorker":
                    cookie.IsWorker = true;
                    break;
                case "IsHr":
                    cookie.IsHr = true;
                    break;
                case "IsAdmin":
                    cookie.IsAdmin = true;
                    break;
            }
            _dictionary.Add(guid, cookie);
            return new[] {guid.ToString(), user.Role};
        }

        //TODO Возможна ошибка что отсутствующие роли будут вылетать, для этого надо будет исправить AddCookie
        /// <summary>
        /// Проверка прав при роутинге GUI.
        /// </summary>
        /// <param name="guidString">Поступивший запрос</param>
        /// <param name="rights">Строка с правами разделенными ';' которым разрешен доступ</param>
        /// <returns>Доступ открыт или закрыт</returns>        
        public bool CheckCookie(string guidString, string rights)
        {
            Guid guid;
            try
            {
                guid = new Guid(guidString);
            }
            catch (Exception)
            {
                return false;
            }
            if (!_dictionary.ContainsKey(guid)) return false;
            var cookie = _dictionary[guid];
            string[] massiveRights = rights.Split(';');
            foreach (var t in massiveRights)
            {
                switch (t)
                {
                    case "IsAbiturient":
                        if (cookie.IsAbiturient)
                        {
                            return true;
                        }
                        break;
                    case "IsStudent":
                        if (cookie.IsStudent)
                        {
                            return true;
                        }
                        break;
                    case "IsStudentLeader":
                        if (cookie.IsStudentLeader)
                        {
                            return true;
                        }
                        break;
                    case "IsTeacher":
                        if (cookie.IsTeacher)
                        {
                            return true;
                        }
                        break;
                    case "IsWorker":
                        if (cookie.IsWorker)
                        {
                            return true;
                        }
                        break;
                    case "IsHr":
                        if (cookie.IsHr)
                        {
                            return true;
                        }
                        break;
                    case "IsAdmin":
                        if (cookie.IsAdmin)
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверка прав на API.
        /// </summary>
        /// <param name="request">Поступивший запрос</param>
        /// <param name="rights">Массив прав которым разрешен доступ</param>
        /// <returns>Доступ открыт или закрыт</returns>        
        public bool CheckCookie(HttpRequest request, string[] rights)
        {
            Guid guid;
            try
            {
                guid = new Guid(request.Headers["Authorization"]);
            }
            catch (Exception)
            {
                return false;
            }
            if (!_dictionary.ContainsKey(guid)) return false;
            var cookie = _dictionary[guid];
            foreach (var t in rights)
            {
                switch (t)
                {
                    case "IsAbiturient":
                        if (cookie.IsAbiturient)
                        {
                            return true;
                        }
                        break;
                    case "IsStudent":
                        if (cookie.IsStudent)
                        {
                            return true;
                        }
                        break;
                    case "IsStudentLeader":
                        if (cookie.IsStudentLeader)
                        {
                            return true;
                        }
                        break;
                    case "IsTeacher":
                        if (cookie.IsTeacher)
                        {
                            return true;
                        }
                        break;
                    case "IsWorker":
                        if (cookie.IsWorker)
                        {
                            return true;
                        }
                        break;
                    case "IsHr":
                        if (cookie.IsHr)
                        {
                            return true;
                        }
                        break;
                    case "IsAdmin":
                        if (cookie.IsAdmin)
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        public User GetUser(HttpRequest request)
        {
            Guid guid;
            try
            {
                guid = new Guid(request.Headers["Authorization"]);
            }
            catch (Exception)
            {
                return null;
            }
            if (!_dictionary.ContainsKey(guid)) return null;
            return _dictionary[guid].User;
        }
    }
}
