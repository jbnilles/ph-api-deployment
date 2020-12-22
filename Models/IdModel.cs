using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class IdModel
    {
        [Required(ErrorMessage = "Id is required")]

        public int id { get; set; }
    }
}
