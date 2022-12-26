using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gsu_math.Models
{
    public class Duyuru
    {
        public int DuyuruId { get; set; }
        public string Baslik { get; set; }
        public string Foto { get; set; }
        public string Metin { get; set; }
    }
}