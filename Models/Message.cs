using System;
namespace ph_UserEnv.Models
{
    public class Message
    {
        public Message()
        {
            status = messageStatus.Sent;
        }
        public enum messageStatus
        {
            Sent,
            Delivered
        }
        public int id { get; set; }
        public string sender_id { get; set; }
        public string receiver_id { get; set; }
        public string message { get; set; }
        public messageStatus status { get; set; }
        public DateTime created_at { get; set; }
    }
}