using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ph_UserEnv.Authentication;

namespace ph_UserEnv.Models
{
    public class ContactClean
    {
        public ContactClean()
        {
            
        }
        
        public string contact_id { get; set; }
        
        public string username { get; set; }
        public string email { get; set; }
        public DateTime created_at { get; set; }

    }
}
