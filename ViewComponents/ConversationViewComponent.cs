#nullable disable
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using webChat.Models;
using webChat.ViewModels;
using webChat.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Session;

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
            var conver = _context.Conversation
                .Include(x => x.Contact)
                .ToList();
            return View(conver);
        }
    }
}
