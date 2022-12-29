using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace gsu_math.Models
{
    public class ForumCevap
    {
        public int ForumCevapId { get; set; }
        public string username { get; set; }
        public int ForumBaslikId { get; set; }
        public string Metin { get; set; }
        [DefaultValue(0)]
        public int BegeniSayisi { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime at_created { get; set; }
        public string faydalibulanlar { get; set; }
    }
}