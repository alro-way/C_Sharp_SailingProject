using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_SailingTemplate.Models 
{
    public class Route
    {
        [Key]
        public int RouteId {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "City must be 2 characters or longer!")]
        public string City {get;set;}
        [Required]
        [MinLength(2, ErrorMessage = "Country must be 2 characters or longer!")]
        public string Country {get;set;}
        [Required]
        [OnlyFutureDate]
        public DateTime ArrivalDate {get;set;}
        [Required]
        [MinLength(10, ErrorMessage = "Country must be 10 characters or longer!")]
        public string Desc {get;set;}
        public string Img {get;set;}


        public DateTime CreatedAt {get;set;}=DateTime.Now;
        public DateTime UpdatedAt {get;set;}=DateTime.Now;

    }


    
    public class OnlyFutureDateAttribute: ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
        {
            if ((DateTime) value < DateTime.Now)
                return new ValidationResult("Date must be in the Future");
            return ValidationResult.Success;
        }

    }
}