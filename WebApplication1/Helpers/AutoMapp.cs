using AutoMapper;
using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class AutoMapp : Profile
    {
        public AutoMapp()
        {
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
