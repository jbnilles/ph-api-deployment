using System;
using System.ComponentModel.DataAnnotations;

namespace ph_UserEnv.Models
{
    public class MessageModel
    {
        
       

        [Required(ErrorMessage = "User Name is required")]
        public string sender_id { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string receiver_id { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string message { get; set; }
    }
}