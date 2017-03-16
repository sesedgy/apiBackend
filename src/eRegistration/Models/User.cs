﻿using System;

namespace eRegistration.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid IdClient { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string HashSalt { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsOnline { get; set; }
        public string WhoUpdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}
