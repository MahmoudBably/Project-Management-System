using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trial.Models
{
    public class Requests
    {   
        [Key]
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int From_ApplicationUserId { get; set; }
        public int To_ApplicationUserId { get; set; }
    }
}