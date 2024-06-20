using Finance.Domain.Enum;

namespace Finance.Domain.Dtos.Requests
{
    public class ExpensesFilterRequest
    {
        public string? Category { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public Currency? Currency { get; set; }
        public bool? Recurring { get; set; }
    }
}
