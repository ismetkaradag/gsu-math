using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using gsu_math.Models;

namespace gsu_math.ViewModels
{
    public class ForumDetailViewModel
    {
        
        public ForumBaslik Baslik { get; set; }
        public List<ForumCevap> Cevaplar { get; set; }
    }
}