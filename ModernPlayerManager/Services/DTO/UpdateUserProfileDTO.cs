﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernPlayerManager.Services.DTO
{
    public class UpdateUserProfileDTO
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
    }
}
