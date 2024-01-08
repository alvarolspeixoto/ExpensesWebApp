using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpensesWebApp.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "Insira um nome")]
        [StringLength(45)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Insira um valor")]
        [DisplayName("Valor")]
        public decimal Value { get; set; }
        [Required(ErrorMessage = "Escolha uma data")]
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        [Required]
        [DisplayName("Despesa recorrente?")]
        public bool Recurrent { get; set; }

        public int? PeriodInDays { get; set; } = null;
        public int? BillingDay { get; set; } = null;

        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; } = null;
        [Required]
        public int GroupId { get; set; }
        public virtual Group? Group { get; set; }
        
    }
}
