using System.ComponentModel.DataAnnotations;

namespace InitalWebAPI.Dtos
{
    public class DogRequestDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
