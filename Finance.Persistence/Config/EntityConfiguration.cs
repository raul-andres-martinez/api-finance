using Finance.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Persistence.Config
{
    public class EntityConfiguration<T> where T : Entity
    {
        public void DefaultConfigs(EntityTypeBuilder<T> builder, string tableName)
        {
            builder.ToTable(tableName);
            builder.HasKey(x => x.Uid);

            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
