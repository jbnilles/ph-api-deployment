using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class GameStateIdModel
    {
        [Required(ErrorMessage = "game_id is required")]

        public int game_id { get; set; }
        [Required(ErrorMessage = "row is required")]
        public int row { get; set; }
        [Required(ErrorMessage = "col is required")]
        public int col { get; set; }
        [Required(ErrorMessage = "update is required")]
        public DateTime update_at { get; set; }

    }
}
