using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoLiAnProject.Models.ViewModels
{
    public class SignupVM
    {
        [Required(ErrorMessage = "Detta fält är obligatoriskt")]
        [DataType(DataType.EmailAddress)] 
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage ="Ange en korrekt epost-adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Detta fält är obligatoriskt, lösenordet måste innehålla minst ett specialtecken")]
        [Display(Name = "Välj lösenord")]
        [DataType(DataType.Password)] //dolda tecken istf synliga tecken
        public string Password { get; set; }

        [Required(ErrorMessage = "Detta fält är obligatoriskt")]
        [DataType(DataType.Password)]
        [Display(Name = "Fyll i lösenord igen")]
        [Compare(nameof(SignupVM.Password))]
        public string PasswordRepeat { get; set; }

        [Required(ErrorMessage = "Detta fält är obligatoriskt")]
        [Display(Name = "Namn")]
        public string FirstName { get; set; }

    }
}
