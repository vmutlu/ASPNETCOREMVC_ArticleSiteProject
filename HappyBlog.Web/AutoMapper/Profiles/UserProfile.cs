using AutoMapper;
using HappyBlog.Entities.Concrete;
using HappyBlog.Entities.DTOs;

namespace HappyBlog.Web.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDTO, User>();
            CreateMap<User, UserUpdateDTO>();
            CreateMap<UserUpdateDTO, User>();
        }
    }
}
