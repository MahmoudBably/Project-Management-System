using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trial.Models
{
    public class ProjectComments
    {   
        [Key]
        public int Id { get; set; }
        public Project Project { get; set; }
        public byte ProjectId { get; set; }
        public string Comment { get; set; }
    }
}