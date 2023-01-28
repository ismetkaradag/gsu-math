using System.Security.Claims;
using gsu_math.Models;
using gsu_math.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;

namespace gsu_math.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        public AccountController(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                var a = _context.User.FirstOrDefault(p => p.Username == user.Username);
                if(a != null)
                {
                    ModelState.AddModelError(nameof(user.Username),"Bu kullanıcı adı daha önce alınmış.");
                    return View(user);
                }else{
                    var b = _context.User.FirstOrDefault(p => p.Email == user.Email);
                    if(a != null)
                    {
                        ModelState.AddModelError(nameof(user.Email),"Bu mail adresi zaten kullanılıyor.");
                        return View(user);
                    }else{
                        string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                        string saltedPassword = user.Password + md5Salt;
                        string hashedPassword = saltedPassword.MD5();
                        user.Password = hashedPassword;
                        user.AtCreated = DateTime.Now;
                        user.Status = "Ogrenci";
                        _context.Add(user);
                        _context.SaveChanges();
                        return RedirectToAction("Login","Account");
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
                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = data.Password + md5Salt;
                string hashedPassword = saltedPassword.MD5();
                var user = _context.User.FirstOrDefault(p=>p.Username == data.Username && p.Password == hashedPassword);
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
                    return RedirectToAction("index","home");

                }
                ModelState.AddModelError(nameof(user.Password),"Şifre veya parola hatalı.");
            }

            return View(data);
        }
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("index","home");
        }
        public IActionResult Profil(){
            return View(_context.User.FirstOrDefault(p => p.Username == User.Identity.Name));
        }
        [HttpPost]
        public IActionResult UpdateProfil(IFormCollection form){
            var user = _context.User.FirstOrDefault(p => p.Username == User.Identity.Name);
            user.AdSoyad = form["AdSoyad"];
            user.Email = form["Email"];
            
            _context.SaveChanges();
            return RedirectToAction("profil");
        }
        public IActionResult Settings(string id)
        {
            var a = _context.User.FirstOrDefault(p => p.Username == id);
            if (a != null)
            {
                return View(a);
            }else
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult UpdateUser(string id,IFormCollection collection)
        {
            var user = _context.User.FirstOrDefault(p => p.Username == id);
            if (user != null)
            {
                user.Email = collection["Email"];
                user.Is_admin = collection["Is_admin"]=="on"?true:false;
                user.Status = collection["Statu"];
                _context.User.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Settings","Account",new{id = id});
            }
            return RedirectToAction("Settings");
        }
    }
}