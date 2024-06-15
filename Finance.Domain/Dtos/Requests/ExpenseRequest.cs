using Finance.Domain.Enum;
using Finance.Domain.Models.Entities;
using System.Text.Json.Serialization;

namespace Finance.Domain.Dtos.Requests
{
    public class ExpenseRequest
    {
        public ExpenseRequest(decimal amount, DateTime date, string category, string? description, PaymentMethod paymentMethod, Currency currency, int? frequencyInDays)
        {
            Amount = amount;
            Date = date;
            Category = category;
            Description = description;
            PaymentMethod = paymentMethod;
            Currency = currency;
            FrequencyInDays = frequencyInDays;
        }

        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Category { get; private set; }
        public string? Description { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public Currency Currency { get; private set; }
        public int? FrequencyInDays { get; private set; }

        public Expense ToEntity(Guid userId)
        {
            return new Expense(Amount, Date, Category, Description, PaymentMethod, Currency, FrequencyInDays, userId);
        }
    }
}
