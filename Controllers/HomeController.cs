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
        public async Task<IActionResult> AddConversetion(string UserName, string NickName, string Server)
        {

            if (ModelState.IsValid && _context.User.Find(UserName) != null)
            {
                Conversation conver = new Conversation()
                {
                    Name = NickName,
                    ContactId = UserName,
                    //Contact = _context.User.Find(UserName),
                    //Image = _context.User.Find(UserName).ProfilePicture
                    //User.Identity.Name
                };
                //string username = User.Identity.Name;
                _context.Conversation.Add(conver);
                var userName = TempData["userName"];
                if(userName != null)
                {
                    User user = _context.User.Find(userName);
                    user.Conversations.Add(conver);
                }
                await _context.SaveChangesAsync();
                //HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Chat", new {id = conver.Id});
            }
            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var userName = TempData["userName"];
            User user = _context.User.Find(userName);

            var conver = _context.Conversation
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
             return View(conver);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int converId, string message)
        {
            var Message = new Message
            {
                ConverId = converId,
                Text = message,
                sendTime = DateTime.Now

            };
            _context.Message.Add(Message);
            await _context.SaveChangesAsync();


            return RedirectToAction("Chat", new {id = converId});
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