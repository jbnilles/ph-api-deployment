﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class UserIdModel
    {
        
        [Required(ErrorMessage = "userId is required")]
        public string userId { get; set; }
    }
}
