using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ph_UserEnv.Authentication;

namespace ph_UserEnv.Models
{
    public class GameSession
    {
        public enum gameStatus
        {
            Matchmaking,
            in_progress,
            completed
        }
        public int id { get; set; }
        public string game_name { get; set; }
        public string creator_id { get; set; }
        public string current_turn_id { get; set; }
        public string winner_id { get; set; }
        public string moves_left { get; set; }
        public string game_state { get; set; }
        public gameStatus status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public virtual ApplicationUser creator { get; set; }
        public virtual ApplicationUser winner { get; set; }
        public virtual ApplicationUser current_turn { get; set; }




    }
}
