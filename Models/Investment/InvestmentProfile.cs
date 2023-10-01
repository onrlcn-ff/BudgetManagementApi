using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BudgetManagementApi.Models
{
    public class InvestmentProfile: Profile
    {
        public InvestmentProfile(){
            CreateMap<Investment,InvestmentDto>();
            CreateMap<InvestmentDto,Investment>();
        }
        
    }
}