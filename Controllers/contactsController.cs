using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webChat.Data;
using webChat.Models;
using webChat.ViewModels;

namespace webChat.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class contactsController : Controller
    {
        private readonly webChatContext _context;

        public contactsController(webChatContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userName = HttpContext.Session.GetString("UserName");
            return Json(await _context.Conversation
                .Where(x => x.UserId == userName)
                .ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact([Bind("id","name","server")] AddContact con)
        {
            if(ModelState.IsValid)
            {
                var userName = HttpContext.Session.GetString("UserName");
                var user = await _context.User
                .FirstOrDefaultAsync(x => x.UserName == userName);
                var contact = await _context.Conversation
                    .FirstOrDefaultAsync(x => x.UserId == userName && x.id == con.id);
                if(contact == null)
                {
                    Conversation conver = new Conversation()
                    {
                        id = con.id,
                        UserId = userName,
                        name = con.name,
                        server = con.server,
                        last = "",
                        lastdate = DateTime.Now,
                        ProfilePicture = user.ProfilePicture,
                    };
                    _context.Conversation.Add(conver);
                    await _context.SaveChangesAsync();
                    return Json(conver);
                }
                return Json(contact);


            }
            return BadRequest();
            //[FromBody] string id, [FromBody] string name, [FromBody] string server)
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsContact(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var userName = HttpContext.Session.GetString("UserName");
            var contact = await _context.Conversation
                .FirstOrDefaultAsync(x => x.UserId == userName && x.id == id);
            if(contact == null)
            {
                return NotFound();
            }
            return Json(contact);
        }

        [HttpPost("{id}")]
        public async void UpdateContact(string id, string name, string server)
        {
            var userName = HttpContext.Session.GetString("UserName");
            var contact = await _context.Conversation
                .FirstOrDefaultAsync(x => x.UserId == userName && x.id == id);
            if (contact != null)
            {
                contact.name = name;
                contact.server = server;
                await _context.SaveChangesAsync();
            }
        }
        
        [HttpDelete("{id}")]
        public async void DeleteContact(string? id)
        {
            if (id != null)
            {
                var userName = HttpContext.Session.GetString("UserName");
                Conversation conver = await _context.Conversation
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.UserId == userName && x.id == id);
                if (conver != null)
                {
                    //var messages = _context.Message;
                       // .Where(x => x.ContactId == id && x.UserId == userName);
                    foreach(var message in conver.Messages)
                    {
                        //_context.Remove(message);
                        conver.Messages.Remove(message);
                    }
                    _context.Remove(conver);
                    await _context.SaveChangesAsync();
                }  
            }
        }

        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetMessages(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userName = HttpContext.Session.GetString("UserName");
            Conversation conver = await _context.Conversation
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.UserId == userName && x.id == id);
            if (conver == null)
            {
                return NotFound();
            }
            var messages = conver.Messages.ToList();
             
            /*var messages = await contact.Messages.
                .Message
                .Where(x => x.ContactId == id && x.UserId == userName)
                .ToListAsync();*/
            return Json(messages);
        }
        //our user send mesage to they user
        [HttpPost("{id}/messages")]
        public async void PostMessages(string? id, string content)
        {
            if (id != null)
            {
                var userName = HttpContext.Session.GetString("UserName");

                var Message = new Message
                {
                    content = content,
                    created = DateTime.Now,
                    sent = true,
                };

                Conversation conver = _context.Conversation
                    .Include(x => x.Messages)
                    .FirstOrDefault(x => x.UserId == userName && x.id == id);
                conver.last = content;
                conver.lastdate = DateTime.Now;
                conver.Messages.Add(Message);
                //_context.Message.Add(Message);
                await _context.SaveChangesAsync();
            }
        }

        [HttpGet("{id}/messages/{id2}")]
        public async Task<IActionResult> GetMessageId(string? id, int? id2)
        {
            if (id == null || id2 == null)
            {
                return NotFound();
            }
            var userName = HttpContext.Session.GetString("UserName");
            Conversation conver = await _context.Conversation
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.UserId == userName && x.id == id);
            if (conver == null)
            {
                return NotFound();
            }
            var message = conver.Messages.FirstOrDefault(x => x.Id == id2);
            return Json(message);
        }

        [HttpPost("{id}/messages/{id2}")]
        public async void PostMessageId(string? id, int? id2, string content)
        {
            if (id != null && id2 != null)
            {
                var userName = HttpContext.Session.GetString("UserName");
                Conversation conver = await _context.Conversation
                    .Include(x => x.Messages)
                    .FirstOrDefaultAsync(x => x.UserId == userName && x.id == id);
                if (conver != null)
                {
                    var message = conver.Messages.FirstOrDefault(x => x.Id == id2);
                    if (message != null)
                    {
                        message.content = content;
                        await _context.SaveChangesAsync();
                    }
                    
                }
            }
        }

        [HttpDelete("{id}/messages/{id2}")]
        public async void DeleteMessageId(string? id, int? id2)
        {
            if (id != null && id2 != null)
            {
                var userName = HttpContext.Session.GetString("UserName");
                Conversation conver = await _context.Conversation
                    .Include(x => x.Messages)
                    .FirstOrDefaultAsync(x => x.UserId == userName && x.id == id);
                if (conver != null)
                {
                    var message = conver.Messages.FirstOrDefault(x => x.Id == id2);
                    if (message != null)
                    {
                        conver.Messages.Remove(message);
                        await _context.SaveChangesAsync();
                    }

                }
            }
        }

        /*
        [HttpPost("/invitations")]
        public async Task<IActionResult> Invitations([Bind("from", "to", "server")] Invite inv)
        {
            if(ModelState.IsValid)
            {
                //var userName = HttpContext.Session.GetString("UserName");
                var contact = await _context.User
                .FirstOrDefaultAsync(x => x.UserName == inv.from);
                string image;
                if (contact != null)
                {
                    image = contact.ProfilePicture;
                }
                else
                {
                    image = "avatar.png";

                }
                Conversation conver = new Conversation()
                {
                    id = inv.from,
                    UserId = inv.to,
                    name = inv.from,
                    server = inv.server,
                    last = "",
                    lastdate = DateTime.Now,
                    ProfilePicture = image,

                };
                _context.Conversation.Add(conver);
                await _context.SaveChangesAsync();
                return Json(conver);
            }
            return NotFound();
        }*/

        [HttpPost("transfer")]
        public async void Transfer(string from, string to, string content)
        {
            if (from != null && to != null && content != null)
            {

                var Message = new Message
                {
                    content = content,
                    created = DateTime.Now,
                    sent = true,
                };

                Conversation conver = _context.Conversation
                    .Include(x => x.Messages)
                    .FirstOrDefault(x => x.UserId == to && x.id == from);
                if (conver != null)
                {
                    conver.last = content;
                    conver.lastdate = DateTime.Now;
                    conver.Messages.Add(Message);
                    //_context.Message.Add(Message);
                    await _context.SaveChangesAsync();
                }

            }

        }
    }
}
