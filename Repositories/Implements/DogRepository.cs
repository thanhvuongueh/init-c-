using InitalWebAPI.Contexts;
using InitalWebAPI.Models;
using InitalWebAPI.Repositories.Interfaces;

namespace InitalWebAPI.Repositories.Implements
{
    public class DogRepository : BaseRepository<Dog>, IDogRepository
    {
        public DogRepository(ProjectContext context)
            : base(context)
        {
        }
    }
}
