using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

//TODO: Change this namespace to match your project
namespace FinalProject.Models
{

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        //TODO:  Add any fields that you need for creating a new user

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        //Additional fields go here
        [StringLength(1, ErrorMessage = "Middle initial must be one letter.")]
        [Display(Name = "M")]
        public String MiddleInitial { get; set; }
        
        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [DisplayFormat(DataFormatString = "{0: (###)-###-####}", ApplyFormatInEditMode = true)]
        public String PhoneNumber { get; set; }

        [Display(Name = "Advantage Number")]
        public Int32 AdvantageNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DateofBirth { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public String Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public String City { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        [RegularExpression(@"^(\d{5})$", ErrorMessage = "Zip code must be 5 digits.")]
        public String Zip { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public States State { get; set; }
        
        public Int32 Miles { get; set; }

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        //NOTE: Here is the logic for putting in a password
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

    public class RegisterEmployeeViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        //Additional fields go here
        [StringLength(1, ErrorMessage = "Middle initial must be one letter.")]
        [Display(Name = "M")]
        public String MiddleInitial { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Employee type is required.")]
        [Display(Name = "Employee Type")]
        public EmpType EmployeeType { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public String Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public String City { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        [RegularExpression(@"^(\d{5})$", ErrorMessage = "Zip code must be 5 digits.")]
        public String Zip { get; set; }


        [Required(ErrorMessage = "State is required.")]
        public States State { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [DisplayFormat(DataFormatString = "{0: (###)-###-####}", ApplyFormatInEditMode = true)]
        public String PhoneNumber { get; set; }

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //NOTE: Here is the logic for putting in a password
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

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String UserID { get; set; }
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