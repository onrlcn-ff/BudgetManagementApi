using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BudgetManagementApi.Models
{
    public class IncomeProfile : Profile
    {
        public IncomeProfile()
        {
            CreateMap<Income, IncomeDto>();
            CreateMap<IncomeDto, Income>();
        }
    }
}
