using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trial.Models
{
    public class Experience
    {
        [Key]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int ASPNETMVC { get; set; }
        public int JavaScript { get; set; }
        public int JQuery { get; set; }
        public int HTML5 { get; set; }
        public int PHP  { get; set; }
        public int JAVA { get; set; }
    }
}