using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gsu_math.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}