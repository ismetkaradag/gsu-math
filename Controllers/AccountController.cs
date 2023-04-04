using System.Net.Mail;
using System.Security.Claims;
using gsu_math.Models;
using gsu_math.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
                        Guid i = Guid.NewGuid();

                        user.unique_string = i.ToString();
                        user.is_validate = false;
                        MailMessage mail = new MailMessage();
                        mail.To.Add(user.Email);
                        mail.From = new MailAddress("noreply@gsumath.com");
                        mail.Subject = "gsumath'a hoşgeldin";

                        string body = "<div style='background-color: black;color:white;margin: 10px;padding: 10px;text-align: center;'><h3>selam "+user.Username+" gsumath`a hoşgeldin,</h3><br><p>aşağıdaki butonu kullanarak hesabını aktive edebilirsin.</p><br><a style='border: 1px solid white;border-radius: 5px; color: white;padding: 4px;text-decoration: none;font-family:Arial, Helvetica, sans-serif' href='https://gsumath.com/Account/ValidateMyMail/"+user.unique_string+"'>Doğrula</a><br><img style='margin-top: 35px;margin-bottom: 60px;width: 180px;' src='https://gsumath.com/logo.png' alt='logo'></div>";
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host="smtp-relay.sendinblue.com";
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("gsumaths2@gmail.com", "L3GnFIw57X4pSU6a");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        _context.Add(user);
                        _context.SaveChanges();
                        
                        return RedirectToAction("SendMailVerification","Account");
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
                if (user != null && user.is_validate)
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
                ModelState.AddModelError(nameof(user.Password),"Şifre veya parola hatalı.Veya mail aktivasyonu yapılmamış.");
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
        [Authorize]
        public IActionResult Profil(){
            return View(_context.User.FirstOrDefault(p => p.Username == User.Identity.Name));
        }
        [HttpPost]
        [Authorize]
        public IActionResult UpdateProfil(IFormCollection form){
            var user = _context.User.FirstOrDefault(p => p.Username == User.Identity.Name);
            user.AdSoyad = form["AdSoyad"];
            user.Email = form["Email"];
            
            _context.SaveChanges();
            return RedirectToAction("profil");
        }
        [Authorize(Roles="admin")]
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
        [Authorize(Roles="admin")]
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
        public IActionResult ValidateMyMail(string id)
        {
            var user = _context.User.FirstOrDefault(p=>p.unique_string == id);
            if (user != null)
            {
                user.is_validate = true;
                _context.SaveChanges();
                return RedirectToAction("MailValidated","Account");
            }else
            {
                return Redirect("Error");
            }

        }
        public IActionResult SendMailVerification()
        {
            return View();
        }
        public IActionResult MailValidated()
        {
            return View();
        }
        public IActionResult RefreshMyPass()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RefreshMyPass(GetMailViewModel data)
        {
            if (ModelState.IsValid)
            {
                var user = _context.User.FirstOrDefault(p=>p.Email == data.mailadres);
                if (user != null)
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(user.Email);
                    mail.From = new MailAddress("noreply@gsumath.com");
                    mail.Subject = "gsumath şifre yenileme";

                    string body = "<div style='background-color: black;color:white;margin: 10px;padding: 10px;text-align: center;'><h3>selam "+user.Username+" gsumath sitesi için şifre yenileme bağlantın aşağıda,</h3><br><p>aşağıdaki butonu kullanarak şifreni yenileyebilirsin</p><br><a style='border: 1px solid white;border-radius: 5px; color: white;padding: 4px;text-decoration: none;font-family:Arial, Helvetica, sans-serif' href='https://gsumath.com/Account/ChangeMyPass/"+user.unique_string+"'>şifremi yenile</a><br><img style='margin-top: 35px;margin-bottom: 60px;width: 180px;' src='https://gsumath.com/logo.png' alt='logo'></div>";
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host="smtp-relay.sendinblue.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("gsumaths2@gmail.com", "L3GnFIw57X4pSU6a");
                    smtp.EnableSsl = true;
                    smtp.Send(mail); 
                    return View("WeSendMail");
                }
            }
            return Redirect("Error");
        }
        public IActionResult ChangeMyPass(string id)
        {
            
            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeMyPass(string id,ResetPassViewModel data)
        {
            if (ModelState.IsValid)
            {
                var user = _context.User.FirstOrDefault(p => p.unique_string == id);
                if (user != null)
                {
                    if (data.Password == data.RePassword)
                    {
                        string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                        string saltedPassword = data.Password + md5Salt;
                        string hashedPassword = saltedPassword.MD5();
                        user.Password = hashedPassword;
                        _context.SaveChanges();
                        return RedirectToAction("login","account");
                    }
                    
                }
            }
            return Redirect("Error");
        }
    }
}