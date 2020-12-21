using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ph_UserEnv.Models;

namespace ph_UserEnv.Models
{
    public class CleanMessage
    {
        
        public int id { get; set; }

        public string sender_username { get; set; }
        public string reciever_username { get; set; }
        public string sender_id { get; set; }
        public string receiver_id { get; set; }
        public string message { get; set; }
        public Message.messageStatus status { get; set; }
        public DateTime created_at { get; set; }
    }
}
