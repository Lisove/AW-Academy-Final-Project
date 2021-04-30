using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoLiAnProject.Models.ViewModels
{
    public class LoginVM
    {
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }


    }
}
