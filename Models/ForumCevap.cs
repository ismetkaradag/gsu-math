using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gsu_math.Models
{
    public class ForumCevap
    {
        public int ForumCevapId { get; set; }
        [ForeignKey("User")]
        public virtual int UserId { get; set; }
        [ForeignKey("ForumBaslik")]
        public virtual int ForumBaslikId { get; set; }
        public string Metin { get; set; }
        public string BegeniSayisi { get; set; }
    }
}