#nullable disable
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using webChat.Models;
using webChat.ViewModels;
using webChat.Data;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
//using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        public IActionResult AccessDenied()
        {
            return View();
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
            if (_context.User.Find(register.UserName) != null)
            {
                @ViewBag.Message = "Please select a diffrent username";
                register.UserName = "";
                return View(register);
            }
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(register);
                User user = new User()
                {
                    Password = register.Password,
                    UserName = register.UserName,
                    NickName = register.NickName,
                    ProfilePicture = uniqueFileName,
                };
                Signin(user);
                _context.Add(user);
                await _context.SaveChangesAsync();
                //TempData["userName"] = register.UserName;
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Home");

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
            }
            else
            {

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
            if (ModelState.IsValid)
            {
                if (_context.User.Any(x => (x.UserName == login.UserName & x.Password == login.Password)))
                {
                    Signin(GetUser(login.UserName));
                    //TempData["userName"] = login.UserName;
                    HttpContext.Session.SetString("UserName", login.UserName);
                    return RedirectToAction("Index", "Home");
                    //return RedirectToAction(nameof(Index));
                } else
                {
                    ViewBag.Message = "Username and/or password are incorrect";
                }
            }

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
                    new Claim(ClaimTypes.Name, user.UserName),
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

        public void Logout()
        {
            HttpContext.SignOutAsync();
        }

    }

}