using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using gsu_math.Models;


namespace gsu_math.ViewModels
{
    public class EtkinlikCreateViewModel
    {
        public string Baslik { get; set; }
        public string Metin { get; set; }
        public IFormFile foto { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime startDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime endDate { get; set; }
    }
}