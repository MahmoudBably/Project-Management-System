using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trial.Models
{
    public class Request
    {   
        [Key]
        public int Id { get; set; }

        public string FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        [InverseProperty("Requests")]
        public ApplicationUser FromUser { get; set; }

        public string ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        public ApplicationUser ToUser { get; set; }

        public string RegardingUserId { get; set; }
        [ForeignKey("RegardingUserId")]
        public ApplicationUser RegardingUser { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public string Type { get; set; }
    }


    public class RequestViewModel
    {
        public ApplicationUser User { get; set; }
        public Request Request { get; set; }
        public Project Project { get; set; }
    }

    public class RequestRemoveViewModel
    {
        public ApplicationUser User { get; set; }
        public ApplicationUser RegardingUser { get; set; }
        public Request Request { get; set; }
        public Project Project { get; set; }
    }
}