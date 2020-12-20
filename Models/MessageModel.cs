using System;
using System.ComponentModel.DataAnnotations;

namespace ph_UserEnv.Models
{
    public class MessageModel
    {
        
       

        

        [Required(ErrorMessage = "receiver_id is required")]
        public string receiver_id { get; set; }
        [Required(ErrorMessage = "message is required")]
        public string message { get; set; }
    }
}