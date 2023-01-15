using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using gsu_math.Models;
using gsu_math.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            return View(_context.ForumBaslik.ToList());
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult Create(ForumBaslik data)
        {
            
            if (ModelState.IsValid)
            {
                var deja = _context.ForumBaslik.FirstOrDefault(p => p.Baslik == data.Baslik);
                if ( deja == null)
                {
                    SlugHelper helper = new SlugHelper();
                    ForumBaslik forum = new ForumBaslik{
                        
                        slug = helper.GenerateSlug(data.Baslik),
                        Baslik = data.Baslik,
                        AtCreated = DateTime.Now,
                        creater = _context.User.FirstOrDefault(p => p.Username == User.Identity.Name).Username,
                    };
                    _context.Add(forum);
                    _context.SaveChanges();
                    return RedirectToAction("index","forum");
                }else{
                    return RedirectToAction("detail","forum",deja.slug);
                }
            }
            ModelState.AddModelError(nameof(data.Baslik),"Bir hata oluştu.");
            return RedirectToAction("index","forum");
        }
        public IActionResult Detail(string id)
        {
            var s = _context.ForumBaslik.FirstOrDefault(p => p.slug == id);
            ForumDetailViewModel n = new ForumDetailViewModel{
                Baslik = s,
                Cevaplar = _context.ForumCevap.Where(p => p.ForumBaslikId == s.ForumBaslikId).ToList(),
            };
            return View(n);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CevapCreate(IFormCollection collection,string id)
        {
            var a = collection["Metin"];
            var baslik = _context.ForumBaslik.FirstOrDefault(p => p.slug == id);
            if (a != "")
            {
                if (User.Identity.IsAuthenticated)
                {
                    ForumCevap cvp = new ForumCevap{
                        username = User.Identity.Name,
                        at_created = DateTime.Now,
                        ForumBaslikId = baslik.ForumBaslikId,
                        Metin = a.ToString(),
                        faydalibulanlar = ",",
                    };
                    _context.Add(cvp);
                    baslik.cevapsayisi += 1;
                    _context.SaveChanges();
                };
            };

            return RedirectToAction("Detail","Forum",new {id});
        }
        [HttpPost]
        
        public string faydali(int id){
            if (User.Identity.IsAuthenticated)
            {
                var s = _context.ForumCevap.FirstOrDefault(p => p.ForumCevapId == id);
                if (!s.faydalibulanlar.Contains(User.Identity.Name))
                {
                    s.faydalibulanlar += User.Identity.Name.ToString() + ",";
                    s.BegeniSayisi += 1;
                    _context.SaveChanges();
                    return "faydali";
                }else{
                    var a = s.faydalibulanlar;
                    
                    var name = User.Identity.Name;
                    var namev = name+",";
                    var l = a.Remove(a.IndexOf(namev),name.Length+1);
                    s.faydalibulanlar = l;
                    s.BegeniSayisi -= 1;
                    _context.SaveChanges();
                    return "degil";
                }
            }
            return "error";
        }
        [Authorize]
        public IActionResult Delete(int id){
            var silinecek = _context.ForumBaslik.FirstOrDefault(p => p.ForumBaslikId == id);
            if (User.Identity.Name == silinecek.creater)
            {
                return View(silinecek);
            }else
            {
                return RedirectToAction("index","home");
            }
            
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirme(int id)
        {
            var silinecek = _context.ForumBaslik.FirstOrDefault(p => p.ForumBaslikId == id);
            if (silinecek.cevapsayisi > 2)
            {
                return RedirectToAction("index","home");
            }else{
                _context.Remove(silinecek);
                foreach (var item in _context.ForumCevap.Where(p => p.ForumBaslikId == id).ToList())
                {
                    _context.Remove(item);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("index","forum"); 
        }
        [Authorize]
        public IActionResult DeleteCevap(int id)
        {
            var us = _context.ForumCevap.FirstOrDefault(p => p.ForumCevapId == id);
            if (User.Identity.Name == us.username)
            {
                return View(us);
            }else
            {
                return RedirectToAction("index","home");
            }
            
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCevapConfirme(int id)
        {
            var silinecek = _context.ForumCevap.FirstOrDefault(p => p.ForumCevapId == id);
            if (User.Identity.Name == silinecek.username)
            {
                var forum = _context.ForumBaslik.FirstOrDefault(p => p.ForumBaslikId == silinecek.ForumBaslikId);
                forum.cevapsayisi -= 1;
                _context.Remove(silinecek);
                _context.SaveChanges();
                return RedirectToAction("detail","forum",new {id = forum.slug});
            }else
            {
                return RedirectToAction("index","home");
            }
            
        }
    }
}