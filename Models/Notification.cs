using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class Notification
    {
        public int id { get; set; }

        public string sender_username { get; set; }
        public string reciever_username { get; set; }
        public string sender_id { get; set; }
        public string receiver_id { get; set; }
        public string notification_type { get; set; }
        public DateTime created_at { get; set; }
    }
}
