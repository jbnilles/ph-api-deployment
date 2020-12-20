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
        int id { get; set; }
        string contact_1_id { get; set; }
        string contact_2_id { get; set; }
        contactStatus status { get; set; }
        DateTime created_at { get; set; }
    }
}
