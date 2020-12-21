using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class GameNameModel
    {
        [Required(ErrorMessage = "gameName is required")]
        public string gameName { get; set; }
    }
}
