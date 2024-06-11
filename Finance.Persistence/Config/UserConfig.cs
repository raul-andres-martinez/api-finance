using Finance.Domain.Interfaces.Config;
using Finance.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Persistence.Config
{
    public class UserConfig : EntityConfiguration<User>, IEntityTypeConfiguration<User>, IEntityConfig
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            DefaultConfigs(builder, tableName: "users");

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(45)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.PasswordSalt)
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasColumnType("text")
                .IsRequired();

            builder
                .HasMany(x => x.Expenses)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
