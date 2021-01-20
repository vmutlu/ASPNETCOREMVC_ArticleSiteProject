using AutoMapper;
using HappyBlog.Entities.Concrete;
using HappyBlog.Entities.DTOs;
using System;

namespace HappyBlog.Services.AutoMapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDTO, Category>().ForMember(destinationMember => destinationMember.CreatedByName, memberOptions => memberOptions.MapFrom(x => DateTime.Now));
            CreateMap<CategoryUpdateDTO, Category>().ForMember(destinationMember => destinationMember.ModifiedDate, memberOptions => memberOptions.MapFrom(x => DateTime.Now));
            CreateMap<Category, CategoryUpdateDTO>();
        }
    }
}
