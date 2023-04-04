using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gsu_math.Controllers
{
    public class BildirimController : Controller
    {
        private readonly MyDbContext _context;
        public BildirimController( MyDbContext context)
        {
            _context = context;    
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public bool is_read(){
            var a = User.Identity.Name;
            if(a.Length>0){
                var s = _context.User.FirstOrDefault(p => p.Username == a);
                if (s != null)
                {
                    
                    foreach (var item in _context.Bildirim.Where(p => p.is_read == false).ToList())
                    {
                        item.is_read = true;
                    }
                    _context.SaveChanges();
                }
            }
            return true;
        }
        [Authorize]
        public JsonResult notifications(){
            var a = _context.Bildirim.Where(p => p.to == User.Identity.Name).OrderByDescending(p => p.At_created).ToList();
            if (a.Count()>20)
            {
                for (int i = 20; i < a.Count; i++)
                {
                    _context.Remove(a[i]);
                }
            }
            _context.SaveChanges();
            
            return Json(a);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}