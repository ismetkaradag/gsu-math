using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gsu_math.Models;
using Slugify;
namespace gsu_math.Controllers
{
    public class BlogController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public BlogController( MyDbContext context,IWebHostEnvironment hostingEnvironment)
        {
            _context = context;    
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View(_context.Blog.Where(p => p.is_active == true).ToList());
        }
        public IActionResult Detail(string id)
        {
            return View(_context.Blog.FirstOrDefault(p => p.slug == id));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                SlugHelper helper = new SlugHelper();
                blog.Author = User.Identity.Name;
                blog.slug = helper.GenerateSlug(blog.Name);
                _context.Add(blog);
                _context.SaveChanges();
                return RedirectToAction("detail","blog",new {id = blog.slug});
            }
            return View("error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}