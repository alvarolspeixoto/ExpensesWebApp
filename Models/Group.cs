using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpensesWebApp.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo não pode ser vazio.")]
        [DisplayName("Nome")]
        public string Name { get; set; }
    }
}
