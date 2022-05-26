using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webChat.Data;

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
                .ToListAsync()
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var userName = HttpContext.Session.GetString("UserName");
            var contact = await _context.Conversation
                .FirstOrDefaultAsync(x => x.UserId == userName && x.ContactId == id);
            if(contact == null)
            {
                return NotFound();
            }
            return Json(contact);
        }

        [HttpDelete("{id}")]
        public async void Delete(string? id)
        {
            if (id != null)
            {
                var userName = HttpContext.Session.GetString("UserName");
                var contact = _context.Conversation
                    .FirstOrDefaultAsync(x => x.UserId == userName && x.ContactId == id);

                if (contact != null)
                {
                    var messages = _context.Message
                        .Where(x => x.ContactId == id && x.UserId == userName);
                    foreach(var message in messages)
                    {
                        _context.Remove(message);
                        //contact.Remove(message);
                    }
                    _context.Remove(contact);
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
            var contact = _context.Conversation
                .FirstOrDefaultAsync(x => x.UserId == userName && x.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            var messages = await _context.Message
                .Where(x => x.ContactId == id && x.UserId == userName)
                .ToListAsync();
            return Json(messages);
        }
    }
}
