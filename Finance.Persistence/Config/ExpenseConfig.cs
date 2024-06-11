using Finance.Domain.Interfaces.Config;
using Finance.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Persistence.Config
{
    public class ExpenseConfig : EntityConfiguration<Expense>, IEntityTypeConfiguration<Expense>, IEntityConfig
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            DefaultConfigs(builder, tableName: "expenses");

            builder.Property(x => x.Amount)
                .HasColumnType("numeric")
                .IsRequired();

            builder.Property(x => x.Date)
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(x => x.Category)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PaymentMethod)
                .HasColumnType("varchar")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasColumnType("varchar")
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(x => x.Recurring)
                .HasColumnType("boolean")
                .IsRequired();

            builder.Property(x => x.FrequencyInDays)
                .HasColumnType("integer")
                .HasMaxLength(3);
        }
    }
}
