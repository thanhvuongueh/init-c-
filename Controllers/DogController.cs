using System.Net.Mime;
using InitalWebAPI.Dtos;
using InitalWebAPI.Models;
using InitalWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessageDistribution.Controllers
{
    [ApiController]
    [Route("/v1/dogs")]
    public class DogController : ControllerBase
    {
        private readonly IDogService _dogService;
        public DogController(IDogService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> GetDogs()
        {
            var result = await _dogService.GetDogs();
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<Dog>> SaveDog(DogRequestDto requestDto)
        {
            var result = await _dogService.SaveDogs(requestDto);
            return Ok(result);
        }
    }
}
