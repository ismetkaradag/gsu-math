using System.Security.Claims;
using gsu_math.Models;
using gsu_math.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index","home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }else{
                List<User> a = _context.User.Where(p => p.Username == user.Username).ToList();
                if(a.Count>0)
                {
                    ModelState.AddModelError(nameof(user.Username),"Bu kullanıcı adı daha önce alınmış.");
                    return View(user);
                }else{
                    List<User> b = _context.User.Where(p => p.Email == user.Email).ToList();
                    if(a.Count>0)
                    {
                        ModelState.AddModelError(nameof(user.Email),"Bu mail adresi zaten kullanılıyor.");
                        return View(user);
                    }else{
                        if (user.Password != user.ConfirmPwd)
                        {
                            ModelState.AddModelError(nameof(user.Password),"Şifreler eşleşmiyor.");
                            return View(user);
                        }else{
                            user.AtCreated = DateTime.Now;
                            user.Status = "Ogrenci";
                            _context.Add(user);
                            _context.SaveChanges();
                            return RedirectToAction("Login","Account");
                        }
                    }
                }
            }
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index","home");
            }
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel data, string ReturnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index","home");
            }
            if (ModelState.IsValid)
            {
                var user = _context.User.FirstOrDefault(p=>p.Username == data.Username && p.Password == data.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("username",user.Username),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Is_admin == true?"admin":"user")
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var authproperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.Now.AddDays(30),
                        RedirectUri = ReturnUrl,
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authproperties);

                }
            }

            return RedirectToAction("index","home");
        }
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("index","home");
        }
    }
}