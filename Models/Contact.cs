using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class Contact
    {
        public Contact()
        {
            status = contactStatus.Pending;
        }
        public enum contactStatus
        {
            Pending,
            Ignored,
            Accepted,
            Rejected
        }
        public int id { get; set; }
        public string contact_1_id { get; set; }
        public string contact_2_id { get; set; }
        public contactStatus status { get; set; }
        public DateTime created_at { get; set; }
    }
}
