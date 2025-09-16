using Microsoft.AspNetCore.Mvc;
using BlogMvcApp.Data;
using BlogMvcApp.Models;

namespace BlogMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogContext _context;

        public HomeController(BlogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Authentication kontrolü
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var posts = _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToList();
            ViewBag.Username = HttpContext.Session.GetString("Name");
            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Authentication kontrolü
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogPost post)
        {
            // Authentication kontrolü
            if (HttpContext.Session.GetString("UserId") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                post.CreatedAt = DateTime.Now;
                _context.BlogPosts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }
    }
}
