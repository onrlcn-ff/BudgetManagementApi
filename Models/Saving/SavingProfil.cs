using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BudgetManagementApi.Models
{
    public class SavingProfil : Profile
    {
        public SavingProfil()
        {
            CreateMap<Saving,SavingDto>();
            CreateMap<SavingDto,Saving>();
        }
    }
}