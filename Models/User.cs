using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace gsu_math.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "İsim Soyisim")]
        public string AdSoyad { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        public string ConfirmPwd { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime AtCreated { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Status { get; set; }
        [DefaultValue(false)]
        public bool Is_admin { get; set; }
        [DefaultValue(false)]
        public bool is_validate { get; set; }
        [DefaultValue(false)]
        public bool is_mail_validated { get; set; }
    }
}