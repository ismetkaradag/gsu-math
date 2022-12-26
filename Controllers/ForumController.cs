using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using gsu_math.Models;
using gsu_math.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Slugify;
namespace gsu_math.Controllers
{
    public class ForumController : Controller
    {

        private readonly MyDbContext _context;
        public ForumController( MyDbContext context)
        {
            _context = context;    
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public bool Create(ForumCreateViewModel data)
        {
            if (ModelState.IsValid)
            {
                if (_context.ForumBaslik.FirstOrDefault(p => p.Baslik == data.Baslik) == null)
                {
                    SlugHelper helper = new SlugHelper();
                    ForumBaslik forum = new ForumBaslik{
                        
                        slug = helper.GenerateSlug(data.Baslik),
                        Baslik = data.Baslik,
                        AtCreated = DateTime.Now,
                        UserId = _context.User.FirstOrDefault(p => p.Username == User.Identity.Name).UserId,
                    };
                    _context.Add(forum);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public IActionResult Detail(string id)
        {
            
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool CevapCreate(ForumCevap data,string id = "")
        {
            if (ModelState.IsValid)
            {
                if (id != "")
                {
                }
                
            }
            return true;
        }
    }
}