using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gsu_math.Controllers
{
    public class PanelController : Controller
    {
        private readonly MyDbContext _context;
        public PanelController( MyDbContext context)
        {
            _context = context;    
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View(_context.Blog.OrderBy(p => p.is_active).ToList());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpPost]
        public bool toggle_is_active(int id){
            var a = _context.Blog.FirstOrDefault(p => p.Id == id);
            System.Console.WriteLine(a.AtCreated);
            if (a != null)
            {
                if (a.is_active)
                {
                    a.is_active = false;
                    _context.SaveChanges();
                    return true;
                }else
                {
                    a.is_active = true;
                    _context.SaveChanges();

                    return true;
                }
            }
            return false;
        }
        
        public IActionResult Users(){
            return View(_context.User.ToList());
        }
    }
}