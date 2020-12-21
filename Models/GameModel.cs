using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class GameModel
    {
        [Required(ErrorMessage = "game_name is required")]

        public string game_name { get; set; }
        [Required(ErrorMessage = "creator_id is required")]

        

        public string game_state { get; set; }
    }
}
