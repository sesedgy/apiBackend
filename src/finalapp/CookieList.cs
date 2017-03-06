using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalapp.Models;

namespace finalapp
{
    public class CookieList
    {

        private static CookieList _instance;
        private readonly Dictionary<Guid,Cookie> _dictionary;

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

        public string AddCookie(User user)
        {
            var guid = new Guid();
            var cookie = new Cookie() {Id = guid, UserId = user.Id,};
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
                case "IsAdmin":
                    cookie.IsAdmin = true;
                    break;
            }
            _dictionary.Add(guid, cookie);
            return guid.ToString();
        }

        //TODO Возможна ошибка что отсутствующие роли будут вылетать, для этого надо будет исправить AddCookie
        public bool CheckCookie(string guid, string[] rights)
        {
            if (!_dictionary.ContainsKey(new Guid(guid))) return false;
            var cookie = _dictionary[new Guid(guid)];
            foreach (var t in rights)
            {
                switch (t)
                {
                    case "IsAbiturient":
                        if (cookie.IsAbiturient) { return true; }
                        break;
                    case "IsStudent":
                        if (cookie.IsStudent) { return true; }
                        break;
                    case "IsStudentLeader":
                        if (cookie.IsStudentLeader) { return true; }
                        break;
                    case "IsTeacher":
                        if (cookie.IsTeacher) { return true; }
                        break;
                    case "IsWorker":
                        if (cookie.IsWorker) { return true; }
                        break;
                    case "IsAdmin":
                        if (cookie.IsAdmin) { return true; }
                        break;
                }
            }
            return false;
        }
        
    }
}
