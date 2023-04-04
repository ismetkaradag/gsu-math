using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gsu_math.ViewModels
{
    public class ResetPassViewModel
    {
        
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre tekrar")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        
    }
}