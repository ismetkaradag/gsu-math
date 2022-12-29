using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using gsu_math.Models;
namespace gsu_math.Models
{
    public class ForumBaslik
    {
        
        public int ForumBaslikId { get; set; }
        [Required]
        public string Baslik { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime AtCreated { get; set; }
        public string slug { get; set; }
        public string creater { get; set; }
        [DefaultValue(0)]
        public int begenisayisi { get; set; }
        [DefaultValue(0)]
        public int cevapsayisi { get; set; }
    }
}