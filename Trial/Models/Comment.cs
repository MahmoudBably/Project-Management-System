using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trial.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string CommentDesc { get; set; }

        public string ProjectManagerId { get; set; }
        [ForeignKey("ProjectManagerId")]
        public ApplicationUser ProjectManager { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

    }
    public class CommentViewModel
    {
        public ApplicationUser User { get; set; }
        public Comment Comment { get; set; }
    }
}