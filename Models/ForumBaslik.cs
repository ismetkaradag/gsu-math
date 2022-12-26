using System;
using System.Collections.Generic;
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
        public string Baslik { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime AtCreated { get; set; }
        [ForeignKey("User")]
        public virtual int UserId { get; set; }
        public string slug { get; set; }
        
    }
}