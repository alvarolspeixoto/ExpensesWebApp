using System.ComponentModel.DataAnnotations;

namespace ExpensesWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe seu usuário.")]
        [Display(Name = "Usuário")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Informe sua senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? Password { get; set; }
        [Display(Name = "Manter logado?")]
        public bool RememberMe { get; set; }
    }
}
