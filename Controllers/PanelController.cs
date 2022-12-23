using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using gsu_math.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gsu_math.Controllers
{
    public class PanelController : Controller
    {   
        private readonly MyDbContext _context;
        public PanelController( MyDbContext context)
        {
            _context = context;    
        }
        bool yetkiliMi(String yetki)
        {
            if (((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Role).Value.Contains(yetki))
            {
                return true;
            }else{
                return false;
            }
        }
        public IActionResult Index()
        {
            
            if (yetkiliMi("b"))
            {
                //yyetkiliyse yaptır
            }else{
                return RedirectToAction("index","home");
            }
            return View();
        }
        public IActionResult YetkiVer()
        {
            
            return View(_context.User.ToList());
        }
        public IActionResult Yetkiler()
        {
            return View(_context.Yetki.ToList());
        }
        public IActionResult YetkiCreate(){
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YetkiCreate(Yetki yetki)
        {
            if(ModelState.IsValid)
            {
                var y = _context.Yetki.FirstOrDefault(p => p.YetkiHarf == yetki.YetkiHarf);
                if (y == null)
                {
                    _context.Add(yetki);
                    _context.SaveChanges();
                    return RedirectToAction("Yetkiler","Panel");
                }
            }else{
                ModelState.AddModelError(nameof(yetki.YetkiHarf),"Hata tekrar deneyin.");
            }
            
            return RedirectToAction("Yetkiler","Panel");
        }
        public IActionResult YetkiEkleSil(string id=null)
        {
            if (id!=null)
            {
                var user = _context.User.FirstOrDefault(p => p.Username == id);
                if (user != null)
                {
                    ViewBag.mevcutyetkiler = _context.Yetki.ToList();
                    return View(user);
                }
            }
            return RedirectToAction("error","home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YetkiEkleSil(string id = null , IFormCollection collection = null)
        {
            string asd = "";
            foreach (var item in collection)
            {
                if (item.Value == "on")
                {
                    asd += item.Key+ ",";
                }
            }
            System.Console.WriteLine(asd);
            if (id!=null)
            {
                var user = _context.User.FirstOrDefault(p => p.Username == id);
                if (user != null)
                {
                    ViewBag.mevcutyetkiler = _context.Yetki.ToList();
                    return View(user);
                }
            }
            return RedirectToAction("error","home");
        }

    }
}