using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace finalapp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid IdClient { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsOnline { get; set; }
        public string WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}
