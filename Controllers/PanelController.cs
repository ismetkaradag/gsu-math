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
    }
}