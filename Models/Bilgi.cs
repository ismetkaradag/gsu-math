using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gsu_math.Models
{
    public class Bilgi
    {
        public int BilgiId { get; set; }
        public string Tür { get; set; }
        public string Başlık { get; set; }
        public string Metin { get; set; }
    }
}