using System.ComponentModel.DataAnnotations;

namespace ExpensesWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Informe seu nome completo")]
        [Display(Name = "Nome completo")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Informe seu usuário")]
        [Display(Name = "Nome de usuário")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Insira seu e-mail")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido!")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Insira uma senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "As senhas não são iguais")]
        public string? ConfirmPassword { get; set; }

    }
}
