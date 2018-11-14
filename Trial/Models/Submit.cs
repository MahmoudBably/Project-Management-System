using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trial.Models
{
    public class Submit
    {
        [Key, Column(Order = 1)]
        public string PmId { get; set; }
        [ForeignKey("PmId")]
        [InverseProperty("Submits")]
        public ApplicationUser Pm { get; set; }

        [Key, Column(Order = 2)]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        


    }
}