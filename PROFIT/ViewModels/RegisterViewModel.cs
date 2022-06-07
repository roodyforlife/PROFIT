using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROFIT.ViewModels
{
    public class RegisterViewModel
    {
        [EmailAddress(ErrorMessage = "Некорректный email")]
        [Required(ErrorMessage = "Это обязательное поле")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Это обязательное поле")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 15 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это обязательное поле")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Номер телефона должен состоять из цифр")]
        public string Phone { get; set; }

        [RegularExpression(@"[A-Za-z0-9_]+", ErrorMessage = "Пароль должен состоять из латинский букв, цифр и символа '_'")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 20 символов")]
        [Required(ErrorMessage = "Это обязательное поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Это обязательное поле")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        public string ReturnUrl { get; set; }
    }
}
