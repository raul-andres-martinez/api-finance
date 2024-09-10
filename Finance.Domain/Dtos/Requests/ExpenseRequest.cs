using Finance.Domain.Enum;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public decimal Amount { get; private set; }
        [Required]
        public DateTime Date { get; private set; }
        [Required]
        public string Category { get; private set; }
        public string? Description { get; private set; }
        [Required]
        public PaymentMethod PaymentMethod { get; private set; }
        [Required]
        public Currency Currency { get; private set; }
        public int? FrequencyInDays { get; private set; }
    }
}