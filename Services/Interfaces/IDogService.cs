using InitalWebAPI.Dtos;
using InitalWebAPI.Models;

namespace InitalWebAPI.Services.Interfaces
{
    public interface IDogService
    {
        public Task<ICollection<Dog>> GetDogs();

        public Task<Dog> SaveDogs(DogRequestDto dogRequestDto);

        public Task DeleteDog(Guid id);
    }
}
