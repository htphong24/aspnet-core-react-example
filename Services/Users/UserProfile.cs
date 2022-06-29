using AutoMapper;
using Common.Identity;
using SqlServerDataAccess.EF;
// ReSharper disable CheckNamespace

namespace Services
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<ApplicationUser, UserModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName));
            CreateMap<UserModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}