using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ph_UserEnv.Authentication;
using ph_UserEnv.Models;

namespace ph_UserEnv.Models
{
    public class GamePlayer
    {
        public int id { get; set; }
        public int gameSession_id { get; set; }
        public string user_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual GameSession gameSession { get; set; }
    }
}