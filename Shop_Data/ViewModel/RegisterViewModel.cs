using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Specify your name")]
        [MaxLength(30, ErrorMessage = "Your name should not be more than 30 characters")]
        [MinLength(3, ErrorMessage = "Your name should be more than 3 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Specify the password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Your password should be more than 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm the password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your passwords should match")]
        public string PasswordConfirm { get; set; }

    }
}
