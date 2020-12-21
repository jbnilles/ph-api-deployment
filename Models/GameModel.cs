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

        public string creator_id { get; set; }
        [Required(ErrorMessage = "current_turn_id is required")]

        public string current_turn_id { get; set; }
        [Required(ErrorMessage = "game_state is required")]

        public string game_state { get; set; }
    }
}
