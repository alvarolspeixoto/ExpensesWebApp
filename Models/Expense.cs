using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesWebApp.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nome")]
        [Required]
        [StringLength(45)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Valor")]
        public decimal Value { get; set; }
        [Required]
        [DisplayName("Despesa recorrente?")]
        public bool Recurrent { get; set; }
        public int PeriodInDays { get; set; }
        public int BillingDay { get; set; }

        public virtual int? CategoryId { get; set; } = null;
        public virtual Category? Categories { get; set; }
        [Required]
        public virtual int GroupId { get; set; }
        public virtual Group? Groups { get; set; }
        
    }
}
