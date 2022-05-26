#nullable disable
using Microsoft.AspNetCore.Mvc;
using webChat.Models;
using webChat.ViewModels;
using webChat.Data;
using Microsoft.EntityFrameworkCore;

namespace webChat.ViewComponents
{
    public class ConversationViewComponent : ViewComponent
    {
        private readonly webChatContext _context;

        public ConversationViewComponent(webChatContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.Session.GetString("UserName");

            var conver = _context.Conversation
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.sendTime)
                .ToList();
            return View(conver);
        }
    }
}
