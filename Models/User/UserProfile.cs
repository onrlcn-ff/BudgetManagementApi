using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BudgetManagementApi.Models.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.Password,
                    opt => opt.MapFrom(src => Encoding.UTF8.GetString(src.PasswordHash))
                );
            CreateMap<UserDto, User>()
                .ForMember(
                    dest => dest.PasswordHash,
                    opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Password))
                );
        }
    }
}
