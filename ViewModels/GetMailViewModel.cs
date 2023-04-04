using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gsu_math.ViewModels
{
    public class GetMailViewModel
    {
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "E-posta")]
        [DataType(DataType.EmailAddress)]
        public string mailadres { get; set; }
        
    }
}