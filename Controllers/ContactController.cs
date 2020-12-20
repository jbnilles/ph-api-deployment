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
               Contact[] contacts = _db.Contacts.Where(X => ((X.contact_1_id == claim || X.contact_2_id == claim) && X.status == Contact.contactStatus.Accepted)).ToArray();
                return Ok(contacts);
            }
            else
            {
                return StatusCode(404);
            }
            
            
        }
        [Route("AddContacts")]
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] string contact_id)
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
                    contact_2_id = contact_id,
                    status = Contact.contactStatus.Pending,
                    created_at = DateTime.Now
                };
                _db.Contacts.Add(contact);
                _db.SaveChanges();
                return Ok();
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
    }
}
