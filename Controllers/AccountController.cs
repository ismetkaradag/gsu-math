using Microsoft.AspNetCore.Mvc;

namespace gsu_math.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;
        public AccountController( MyDbContext context)
        {
            _context = context;    
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}