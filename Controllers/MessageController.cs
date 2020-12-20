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
        [AllowAnonymous]
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
                Message[] messages = _db.Messages.Where(X => (X.receiver_id == claim || X.sender_id == claim)).Where(X => (X.receiver_id == userId.userId || X.sender_id == userId.userId)).OrderBy(x => x.created_at).ToArray();
                return Ok(messages);
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
        
    }
}
