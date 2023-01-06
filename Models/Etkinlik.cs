using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gsu_math.Models
{
    public class Etkinlik
    {
        public int EtkinlikId { get; set; }
        public string Baslik { get; set; }
        public string Metin { get; set; }
        public string Foto { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime atCreated { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime startDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime endDate { get; set; }

    }
}