using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class GameIdModel
    {
        [Required(ErrorMessage = "game_id is required")]
        public int game_id { get; set; }
    }
}
