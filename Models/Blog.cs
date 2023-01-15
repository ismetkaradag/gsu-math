using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gsu_math.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime AtCreated { get; set; }=DateTime.Now;
        [DataType(DataType.Text)]
        public string Metin { get; set; }
        [DefaultValue(false)]
        public bool is_active { get; set; }
        public string slug { get; set; }

    }
}