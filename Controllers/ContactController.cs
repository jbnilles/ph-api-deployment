using ph_UserEnv.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ph_UserEnv.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ph_UserEnv.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public ContactController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration,ApplicationDbContext db)
        {
            _db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        
        [Route("getContacts")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);
               Contact[] contacts = _db.Contacts.Where(X => ((X.contact_1_id == claim ) && X.status == Contact.contactStatus.Accepted)).ToArray();
                List<ContactClean> contactCleans = new List<ContactClean>();
                foreach(Contact c in contacts)
                {
                    string id = c.contact_1_id;
                    if(claim == c.contact_1_id)
                    {
                        id = c.contact_2_id;
                    }
                    ApplicationUser au = _db.Users.Where(x => x.Id == id).FirstOrDefault();
                    contactCleans.Add(new ContactClean { contact_id = au.Id, username = au.UserName, created_at = c.created_at, email = au.Email });
                }
                return Ok(contactCleans);
            }
            else
            {
                return StatusCode(404);
            }
            
            
        }
        [Route("AddContact")]
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] UserIdModel userId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);
                Contact contact = new Contact{
                    contact_1_id = claim,
                    contact_2_id = userId.userId,
                    status = Contact.contactStatus.Accepted,
                    created_at = DateTime.Now
                };
                if (_db.Contacts.Where(x => (x.contact_1_id == claim && x.contact_2_id == userId.userId) ).Count() == 0)
                {
                    _db.Contacts.Add(contact);
                                    _db.SaveChanges();
                }
                
                return Ok(contact);
            }
            else
            {
                return StatusCode(404);
            }


        }

        [Route("RejectContacts")]
        [Route("RemoveContacts")]
        [HttpPost]
        public async Task<IActionResult> RemoveContact([FromBody] string contact_id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);
                Contact contact = _db.Contacts.Where(x => (x.contact_1_id == claim && x.contact_2_id == contact_id) || (x.contact_1_id == contact_id && x.contact_2_id == claim)).FirstOrDefault();
                contact.status = Contact.contactStatus.Rejected;
                _db.Contacts.Update(contact);
                _db.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(404);
            }


        }
        [Route("AcceptContacts")]
        [HttpPost]
        public async Task<IActionResult> AcceptContact([FromBody] string contact_id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);
                Contact contact = _db.Contacts.Where(x => (x.contact_1_id == claim && x.contact_2_id == contact_id) || (x.contact_1_id == contact_id && x.contact_2_id == claim)).FirstOrDefault();
                contact.status = Contact.contactStatus.Accepted;
                _db.Contacts.Update(contact);
                _db.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(404);
            }


        }
        [Route("getNotifications")]
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);
                Message[] messages = _db.Messages.Where(X => X.receiver_id == claim && X.status == Message.messageStatus.Sent).OrderByDescending(x => x.created_at).ToArray();
                List<Notification> notifications = new List<Notification>();
                foreach (Message m in messages)
                {
                    ApplicationUser sender = _db.Users.Where(x => x.Id == m.sender_id).FirstOrDefault();
                    ApplicationUser reciever = _db.Users.Where(x => x.Id == m.receiver_id).FirstOrDefault();
                    notifications.Add(new Notification { created_at = m.created_at, id = m.id, notification_type = "message", receiver_id = m.receiver_id, sender_id = m.sender_id,  reciever_username = reciever.UserName, sender_username = sender.UserName });
                }
                return Ok(notifications);
            }
            else
            {
                return StatusCode(404);
            }


        }
    }
}
