using Microsoft.AspNetCore.Mvc;
using BlogMvcApp.Data;
using BlogMvcApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace BlogMvcApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly BlogContext _context;

        public AuthController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Eğer zaten giriş yapmışsa ana sayfaya yönlendir
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kullanıcıyı veritabanından bul
                    var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);
                    
                    if (user != null && VerifyPassword(model.Password, user.Password))
                    {
                        // Session'a kullanıcı bilgilerini kaydet
                        HttpContext.Session.SetString("UserId", user.Id.ToString());
                        HttpContext.Session.SetString("Username", user.Username);
                        HttpContext.Session.SetString("Name", user.Name);
                        
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
                    }
                }
                catch (Exception ex)
                {
                    // Veritabanı hatası durumunda
                    ModelState.AddModelError("", "Veritabanı hatası. Lütfen migration oluşturun: Add-Migration AddUserTable");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
    }
}
