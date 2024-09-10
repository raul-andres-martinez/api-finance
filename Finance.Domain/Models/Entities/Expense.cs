using Finance.Domain.Enum;

namespace Finance.Domain.Models.Entities
{
    public class Expense : Entity
    {
        public Expense(decimal amount, DateTime date, string category, string? description, PaymentMethod paymentMethod, Currency currency, int? frequencyInDays, Guid userId)
        {
            Amount = amount;
            Date = date;
            Category = category;
            Description = description;
            PaymentMethod = paymentMethod;
            Currency = currency;
            Recurring = frequencyInDays == null ? false : true;
            FrequencyInDays = frequencyInDays;
            UserId = userId;
        }

        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Category { get; private set; }
        public string? Description { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public Currency Currency { get; private set; }
        public bool Recurring { get; private set; }
        public int? FrequencyInDays { get; private set; }

        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
    }
}