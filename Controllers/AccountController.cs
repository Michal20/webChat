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

namespace webChat.Controllers
{
    public class AccountController : Controller
    {
        private readonly webChatContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(webChatContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
             webHostEnvironment = hostEnvironment; 
        }

        // GET: RegisterUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,NickName,Password,ConfirmPassword,ProfileImage")] RegisterUser register)
        {
            if(_context.User.Find(register.UserName) != null)
            {
                @ViewBag.Message = "Please select a diffrent username";
                register.UserName = "";
                return View(register);
            }
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(register);
                User user = new User() { 
                    Password = register.Password,
                    UserName = register.UserName,
                    NickName = register.NickName,
                    ProfilePicture = uniqueFileName,
                };
                _context.Add(user);
                await _context.SaveChangesAsync();
                Signin(user);
                TempData["userName"] = register.UserName;
                //HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Home");

                //return RedirectToAction(nameof(Chat));
            }
            return View(register);
        }

        private string UploadedFile(RegisterUser register)
        {
            string uniqueFileName = null;

            if (register.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string type = register.ProfileImage.ContentType;

                uniqueFileName = Guid.NewGuid().ToString() + "_" + register.UserName + "." + type.Substring(6);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    register.ProfileImage.CopyTo(fileStream);
                }
            } else {

                uniqueFileName = "avatar.png";
            }
            return uniqueFileName;
        }


      
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("UserName,Password")] LoginUser login)
        {

            if (_context.User.Any(x => (x.UserName == login.UserName & x.Password == login.Password)))
            {
                //HttpContext.Session.SetString("UserName", login.UserName);
                Signin(GetUser(login.UserName));
                //HttpContext.Session.SetString("UserName", user.UserName);
                TempData["userName"] = login.UserName;
                return RedirectToAction("Index", "Home");
                //return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = "Username and/or password are incorrect";
            return View(login);
        }

        public User GetUser(string UserName)
        {
            return _context.User.Find(UserName);
        }

        private async void Signin(User user)
        {
            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15);
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

    }
    
}
