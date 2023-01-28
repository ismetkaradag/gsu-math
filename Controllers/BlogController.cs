using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gsu_math.Models;
using Slugify;
using System.Net.Mail;

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
        public IActionResult Delete(int id){
            return View(_context.Blog.FirstOrDefault(p => p.Id == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirme(Blog blog){
            if (ModelState.IsValid)
            {
                _context.Blog.Remove(blog);
                _context.SaveChanges();
                
            }
            return RedirectToAction("blog","panel");
        }
        public IActionResult duzenleme_talebi(int id){
            var a = _context.Blog.FirstOrDefault(p => p.Id == id);
            return View(a);
        }
        [HttpPost]
        public IActionResult duzenleme_talebi( IFormCollection coll, string id){
            var metin = coll["Metin"];
            var blog = _context.Blog.FirstOrDefault(p => p.slug == id);
            if (User.Identity.Name != blog.Author)
            {
                Bildirim b = new Bildirim{
                from = User?.Identity.Name,
                to = blog.Author,
                Name = "Yazmış olduğun blog hakkında geri dönüş yapıldı.",
                controller = "Blog",
                action = "detail",
                otherid = blog.slug};
                _context.Add(b);
            
            }
            blog.duzenlememetni = metin;
            blog.in_editing = "Düzenleme istendi";
            blog.is_active = false;
            _context.SaveChanges();

            return RedirectToAction("blog","panel");
        }
        public IActionResult returnedBlog( IFormCollection coll, string id){
            var blog = _context.Blog.FirstOrDefault(p => p.slug == id);
            blog.in_editing="Düzenlemeden döndü";
            blog.Metin = coll["dondurulenMetin"];
            _context.SaveChanges();
            return RedirectToAction("index","blog");

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}