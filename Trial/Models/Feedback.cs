using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trial.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public string EvaluatingTLId { get; set; }
        [ForeignKey("EvaluatingTLId")]
        [InverseProperty("Feedbacks")]
        public ApplicationUser EvaluatingTL { get; set; }


        public string EvaluatedJDId { get; set; }
        [ForeignKey("EvaluatedJDId")]
        public ApplicationUser EvaluatedJD { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public int Communication { get; set; }
        public int TeamWork { get; set; }
        public int ProblemSolving { get; set; }
        public int WorkKnowledge { get; set; }
        public int IndependantAction { get; set; }

        public string FeedbackDesc { get; set; }

    }

    public class FeedbackViewModel
    {
        public ApplicationUser User { get; set; }
        public Feedback Feedback { get; set; }

    }
}