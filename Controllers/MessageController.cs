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

namespace ph_UserEnv.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public MessageController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext db)
        {
            _db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }


        [Route("getMessages")]
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
                Message[] messages = _db.Messages.Where(X => (X.receiver_id == claim || X.sender_id == claim) ).OrderBy(x => x.created_at).ToArray();
                return Ok(messages);
            }
            else
            {
                return StatusCode(404);
            }


        }
        [Route("getMessagesFrom")]
        [HttpPost]
        public async Task<IActionResult> GetMessagesFrom([FromBody] UserIdModel userId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);
                Message[] messages = _db.Messages.Where(X => (X.receiver_id == claim || X.sender_id == claim)).Where(X => (X.receiver_id == userId.userId || X.sender_id == userId.userId)).OrderByDescending(x => x.created_at).Take(25).ToArray();
                List<CleanMessage> cMessages = new List<CleanMessage>();
                foreach(Message m in messages)
                {
                    if(m.status == Message.messageStatus.Sent)
                    {
                        m.status = Message.messageStatus.Delivered;
                        _db.Messages.Update(m);
                    }
                    ApplicationUser sender = _db.Users.Where(x => x.Id == m.sender_id).FirstOrDefault();
                    ApplicationUser reciever = _db.Users.Where(x => x.Id == m.receiver_id).FirstOrDefault();
                    cMessages.Add(new CleanMessage { created_at = m.created_at, id = m.id, message = m.message, receiver_id = m.receiver_id, sender_id = m.sender_id, status = m.status, reciever_username = reciever.UserName, sender_username = sender.UserName });
                }
                _db.SaveChanges();
                return Ok(cMessages);
            }
            else
            {
                return StatusCode(404);
            }


        }
        [Route("SendMessage")]
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageModel messageModel)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);
                Message message = new Message
                {
                    sender_id = claim,
                    receiver_id = messageModel.receiver_id,
                    status = Message.messageStatus.Sent,
                    created_at = DateTime.Now,
                    message = messageModel.message
                };
                _db.Messages.Add(message);
                _db.SaveChanges();
                return Ok(message);
            }
            else
            {
                return StatusCode(404);
            }


        }

        
        [Route("RemoveMessage")]
        [HttpPost]
        public async Task<IActionResult> RemoveMessage([FromBody] string message_id)
        {
            
                Message message = _db.Messages.Where(x => x.id == Int32.Parse(message_id)).FirstOrDefault();
               
                _db.Messages.Remove(message);
                _db.SaveChanges();
                return Ok();

        }

        [Route("getNewMessages")]
        [HttpGet]
        public async Task<IActionResult> GetNewMessages()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);
                Message[] messages = _db.Messages.Where(X => X.receiver_id == claim && X.status == Message.messageStatus.Sent ).OrderByDescending(x => x.created_at).ToArray();
                List<CleanMessage> cMessages = new List<CleanMessage>();
                foreach (Message m in messages)
                {
                    ApplicationUser sender = _db.Users.Where(x => x.Id == m.sender_id).FirstOrDefault();
                    ApplicationUser reciever = _db.Users.Where(x => x.Id == m.receiver_id).FirstOrDefault();
                    cMessages.Add(new CleanMessage { created_at = m.created_at, id = m.id, message = m.message, receiver_id = m.receiver_id, sender_id = m.sender_id, status = m.status, reciever_username = reciever.UserName, sender_username = sender.UserName });
                }
                return Ok(cMessages);
            }
            else
            {
                return StatusCode(404);
            }


        }

    }
}
