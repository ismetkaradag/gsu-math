using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
namespace gsu_math.Models
{
    public class Bildirim
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Yazi { get; set; }
        public string from { get; set; }
        [DefaultValue(false)]
        public bool is_read { get; set; }
        public string to { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string otherid { get; set; }
        public DateTime At_created { get; set; } = DateTime.Now;
    }
}