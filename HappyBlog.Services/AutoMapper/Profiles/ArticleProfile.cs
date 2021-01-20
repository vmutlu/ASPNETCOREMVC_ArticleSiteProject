using AutoMapper;
using HappyBlog.Entities.Concrete;
using HappyBlog.Entities.DTOs;
using System;

namespace HappyBlog.Services.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleAddDTO, Article>().ForMember(destinationMember => destinationMember.CreatedDate, memberOptions => memberOptions.MapFrom(x => DateTime.Now));//ArticleAddDTO sınıfı içine Article sınıfından yer alan CreatedDate propertiesini ekleme işlemi.
            CreateMap<ArticleUpdateDTO, Article>().ForMember(destinationMember => destinationMember.ModifiedDate, memberOptions => memberOptions.MapFrom(x => DateTime.Now));
        }
    }
}
