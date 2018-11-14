using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trial.Models
{


    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }

        [Required]
        [Display(Name = "Job Description")]
        public string Job_Description { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Required]
        [Display(Name = "Choose Your Role")]
        public string UserRoles { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class EditUserViewModel
    {
        public EditUserViewModel() { }

        // Allow Initialization with an instance of ApplicationUser:
        public EditUserViewModel(ApplicationUser user)
        {
                this.Id = user.Id;
                this.UserName = user.UserName;
                this.JobDescription = user.Job_Description;
                this.FirstName = user.Fname;
                this.LastName = user.Lname;
                this.PhoneNum = user.PhoneNum;
                this.Email = user.Email;
            
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]

        public string JobDescription { get; set; }


        [Required]

        public string FirstName { get; set; }

        [Required]

        public string LastName { get; set; }

        [Required]
        public string PhoneNum { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }




    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
