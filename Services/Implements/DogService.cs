using AutoMapper;
using InitalWebAPI.Dtos;
using InitalWebAPI.Exceptions;
using InitalWebAPI.Models;
using InitalWebAPI.Repositories.Interfaces;
using InitalWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InitalWebAPI.Services.Implements
{
    public class DogService : IDogService
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DogService(
            IDogRepository dogRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ICollection<Dog>> GetDogs()
        {
            return await _dogRepository.Read().ToListAsync();
        }

        public async Task<Dog> SaveDogs(DogRequestDto dogRequestDto)
        {
            var dog = _mapper.Map<Dog>(dogRequestDto);
            if (dog.Id == Guid.Empty)
            {
                return await CreateReportTemplate(dog);
            }
            else
            {
                return await UpdateReportTemplate(dog);
            }
        }

        public async Task DeleteDog(Guid id)
        {
            Dog dog = await GetDogById(id);
            await _dogRepository.Delete(dog);
        }

        private async Task<Dog> CreateReportTemplate(Dog dog)
        {
            Dog createdDog = await _dogRepository.Create(dog);
            return createdDog;
        }

        private async Task<Dog> UpdateReportTemplate(Dog dog)
        {
            Dog updatedReportTemplate = await _dogRepository.Update(dog);
            return updatedReportTemplate;
        }

        private async Task<Dog> GetDogById(Guid id)
        {
            Dog? dog = await _dogRepository
                .Read()
                .Where(dog => dog.Id == id)
                .FirstOrDefaultAsync();
            if (dog == null)
            {
                throw new DBRecordNotFoundException("Not found Dog");
            }

            return dog;
        }
    }
}
