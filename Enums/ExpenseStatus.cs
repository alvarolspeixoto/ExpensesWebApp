

using System.ComponentModel.DataAnnotations;

namespace ExpensesWebApp.Enums
{
    public enum ExpenseStatus
    {
        [Display(Name = "Pago")]
        Paid = 1,
        [Display(Name = "Pendente")]
        Pending = 0
    }

}
