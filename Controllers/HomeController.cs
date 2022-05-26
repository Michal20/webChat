using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using webChat.Models;
using webChat.ViewModels;
using webChat.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly webChatContext _context;

        public HomeController(webChatContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AddConversetion(string UserName, string NickName, string server)
        {
            if (_context.User.Find(UserName) != null)
            {
                var userId = HttpContext.Session.GetString("UserName");
                Conversation conver = new Conversation()
                {
                    ContactId = UserName,
                    UserId = userId,
                    Name = NickName,
                    Server = server,
                    Text = "",
                    sendTime = DateTime.Now,
                    ProfilePicture = _context.User.Find(userId).ProfilePicture,
                };
                _context.Conversation.Add(conver);
                await _context.SaveChangesAsync();

                return RedirectToAction("Chat", "Home", new { id = conver.ContactId });
            }
            else
            {
                @ViewBag.MessageUserName = "Username are incorrect";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Chat(string id)
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            var conver = _context.Conversation
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.UserId == userName && x.ContactId == id);
            return View(conver);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(string contactId, string message)
        {
            var userName = HttpContext.Session.GetString("UserName");

            var Message = new Message
            {
                UserId = userName,
                ContactId = contactId,
                Text = message,
                sendTime = DateTime.Now,
                sent = true,
            };
            var conver = _context.Conversation.Find(userName, contactId);
            conver.Text = message;
            conver.sendTime = DateTime.Now;
            conver.Messages.Add(Message);
            _context.Message.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", "Home", new { id = contactId });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}