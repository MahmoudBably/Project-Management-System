using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Trial.Models
{


    public class ProjectHistory
    {
        public ProjectHistory()
        {

        }
        public ProjectHistory(ProjectHistory project)
        {

            this.Id = project.Id;
            this.Name =project.Name;
            this.CustomerId = project.CustomerId;
            this.Price = project.Price;
            this.Shcedule_From = project.Shcedule_From;
            this.Schedule_To = project.Schedule_To;
            this.Description = project.Description;
            this.Status = project.Status;
            this.Assigned = project.Assigned;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUser Customer { get; set; }
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Shcedule_From { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Schedule_To { get; set; }

        public string Description { get; set; }
        public bool Status { get; set; }
        public bool Assigned { get; set; }
        public IQueryable<Project> Pro { get; set; }

    }


    public class DeliveredProjectViewModel
    {
        public ApplicationUser User { get; set; }
        public Worked_on Worked_on { get; set; }
        public ProjectHistory ProjectHistory { get; set; }
    }

    public class ProjectWorkedOnViewModel
    {
        public Worked_on Worked_on { get; set; }
        public ProjectHistory ProjectHistory { get; set; }
    }



}