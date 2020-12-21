using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class UsernameModel
    {
        [Required(ErrorMessage = "userName is required")]
        public string userName { get; set; }
    }
}
