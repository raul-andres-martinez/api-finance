using AutoMapper;
using Finance.Domain.Profiles;

namespace Finance.Test.Mocks.Configs
{
    public class MapperConfigs
    {
        public static IMapper Setup()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ExpenseProfile>();
            });

            var mapper = configuration.CreateMapper();

            return mapper;
        }
    }
}