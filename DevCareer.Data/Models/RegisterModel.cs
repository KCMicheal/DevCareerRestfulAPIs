﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCareer.Data.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }
        public string Role { get; set; } = AppRoles.user;
    }
}
