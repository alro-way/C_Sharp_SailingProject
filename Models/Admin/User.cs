using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_SailingTemplate.Models 
{
    public class User 
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "First Name must be 2 characters or longer!")]
        public string FirstName {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "Last Name must be 2 characters or longer!")]
        public string LastName {get;set;}
        [Required]
        public string Status {get;set;}
        [EmailAddress]
        [Required]
        public string Email {get;set;}
        [DataType(DataType.Password)]
        [Required]
        // [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[!@#$%]).{8,30}$", ErrorMessage = "Password should be a min length of 8 characters, contain at least 1 number, 1 letter, and a special character:  !@#$%")]
        public string Password {get;set;}
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        public DateTime CreatedAt {get;set;}=DateTime.Now;
        public DateTime UpdatedAt {get;set;}=DateTime.Now;


    }
}