using AutoMapper;
using InitalWebAPI.Dtos;
using InitalWebAPI.Models;

namespace InitalWebAPI.Mappers
{
    public class DogMapper : Profile
    {
        public DogMapper()
        {
            CreateMap<DogRequestDto, Dog>();
            CreateMap<Dog, DogResponseDto>();
        }
    }
}
