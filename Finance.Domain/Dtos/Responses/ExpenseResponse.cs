using Finance.Domain.Enum;

namespace Finance.Domain.Dtos.Responses
{
    public class ExpenseResponse
    {
        public ExpenseResponse(decimal amount, DateTime date, string category, string? description, PaymentMethod paymentMethod, Currency currency, bool recurring, int? frequencyInDays)
        {
            Amount = amount;
            Date = date;
            Category = category;
            Description = description;
            PaymentMethod = paymentMethod;
            Currency = currency;
            Recurring = recurring;
            FrequencyInDays = frequencyInDays;
        }

        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Category { get; private set; }
        public string? Description { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public Currency Currency { get; private set; }
        public bool Recurring { get; private set; }
        public int? FrequencyInDays { get; private set; }
    }
}
