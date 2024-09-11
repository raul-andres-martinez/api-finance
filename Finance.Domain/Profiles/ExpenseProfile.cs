using AutoMapper;
using Finance.Domain.Dtos.Requests;
using Finance.Domain.Dtos.Responses;
using Finance.Domain.Models.Entities;

namespace Finance.Domain.Profiles
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<Expense, ExpenseResponse>();
            CreateMap<ExpenseRequest, Expense>()
                .ForMember(p => p.Recurring, x => x.MapFrom(x => x.FrequencyInDays));
        }
    }
}