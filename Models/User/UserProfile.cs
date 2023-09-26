using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BudgetManagementApi.Models.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto,User>();
            CreateMap<User, UserDto>();
        }
    }
}