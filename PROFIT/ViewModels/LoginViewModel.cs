using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROFIT.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Некорректный email")]
        [Required(ErrorMessage = "Это обязательное поле")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Это обязательное поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
