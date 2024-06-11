using Finance.Domain.Interfaces.Config;
using Finance.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence.Context
{
    public class FinanceContext : DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntityConfig).IsAssignableFrom(x) && !x.IsAbstract).ToList();

            foreach (var type in typesToRegister)
            {
                if (type == null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                dynamic configurationInstance = Activator.CreateInstance(type)
                    ?? throw new InvalidOperationException($"Cannot create an instance of type {type.FullName}");
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
