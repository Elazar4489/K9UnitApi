using K9UnitApi.DTOs;
using K9UnitApi.Models;
using K9UnitApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace K9UnitApi.Controllers
{
    [ApiController]
    [Route("[controller]/api")]
    public class K9UnitController :ControllerBase
    {
        private readonly IK9UnitRepository _repository;
        public K9UnitController(IK9UnitRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("dogs")]
        public async Task<IActionResult> CreateDog([FromBody] CreateDogDto dto, [FromQuery] string status = "InTraining")
        {
            var dog = new Dog
            {
                Name = dto.Name,
                Breed = dto.Breed,
                MicrochipId = dto.MicrochipId,
                DateOfBirth = dto.DateOfBirth,
                Specialty = dto.Specialty,
                Status = status
            };

            var createdDog = await _repository.CreateDogAsync(dog);
            if (createdDog is null)
            {
                return BadRequest();
            }

            var responseDto = new DogResponseDto
            {
                Id = createdDog.Id,
                Name = createdDog.Name,
                Breed = createdDog.Breed,
                MicrochipId = createdDog.MicrochipId,
                DateOfBirth = createdDog.DateOfBirth,
                Specialty = createdDog.Specialty,
                Status = createdDog.Status
            };

            return CreatedAtAction(nameof(GetDogById), new { id = responseDto.Id }, responseDto);
        }
        [HttpPost("training-sessions")]

        public async Task<IActionResult> CreateTrainingSession([FromBody] CreateTrainigDto dto, [FromQuery] int dogId)
        {
            if (await _repository.GetDogById(dogId) is null)
            {
                return NotFound();
            }
            var trainingSession = new TrainingSession
            {
                SessionDate = dto.SessionDate,
                DurationMinutes = dto.DurationMinutes,
                TrainingType = dto.TrainingType,
                PerformanceScore = dto.PerformanceScore,
                Evaluator = dto.Evaluator,
                DogId = dogId
            };
            if (dto.PerformanceScore >= 75)
            {
                trainingSession.Passed = true;
            }
            else
            {
                trainingSession.Passed = false;
            }
            var createdtrainingSession = await _repository.CreateTrainingSessionAsync(trainingSession);
            if (createdtrainingSession is null)
            {
                return BadRequest();
            }
            var responseDto = new TrainigResponseDto
            {
                SessionDate = createdtrainingSession.SessionDate,
                DurationMinutes= createdtrainingSession.DurationMinutes,
                TrainingType = createdtrainingSession.TrainingType,
                PerformanceScore = createdtrainingSession.PerformanceScore,
                Passed = createdtrainingSession.Passed,
                Evaluator = createdtrainingSession.Evaluator
            };

            return StatusCode(201, responseDto);
        }



        [HttpGet("/dogs/{id}")]
        public async Task<IActionResult> GetDogById(int id)
        {
            var dog = await _repository.GetDogById(id);
            if (dog is null)
            {
                return NotFound();
            }
            return Ok(dog);
        }
        [HttpDelete("handlers/{handlerId}")]
        public async Task<IActionResult> DeleteTrainingSession(int handlerId)
        {
            var dog = await _repository.Delete(handlerId);
            if (!dog)
            {
                return NotFound();
            }
            return NoContent();
        }




        [HttpGet("dogs/search")]
        public async Task<ActionResult<IEnumerable<SearchForDogsByFiltersDto>>> SearchForDogsByFilters([FromQuery]string? specialty, [FromQuery] string? status)
        {
            var result = await _repository.SearchForDogsByFiltersAsync(specialty, status);
            return Ok(result);
        }
        [HttpGet("dogs/with-handler")]
        public async Task<ActionResult<IEnumerable<DogsWithHandlerDetailsDto>>> GetDogsWithHandlerDetails()
        {
            var result = await _repository.GetDogsWithHandlerDetailsAsync();
            return Ok(result);
        }
        [HttpGet("dogs/performance-summary")]
        public async Task<ActionResult<IEnumerable<PerformanceSummaryDto>>> GetPerformanceSummary()
        {
            var result = await _repository.GetPerformanceSummaryAsync();
            return Ok(result);
        }
        [HttpGet("training-sessions/detailed")]
        public async Task<ActionResult<IEnumerable<TrainingSessionsWithDetailed>>> GetTrainingSessionsWithDetailed()
        {
            var result = await _repository.GetTrainingSessionsWithDetailedAsync();
            return Ok(result);
        }
        [HttpGet("training-sessions/paged")]
        public async Task<ActionResult<TrainingSessionsPagedDto<TrainingSessionsItemDto>>> GetTrainingSessionsPaged([FromQuery]int page = 1, [FromQuery]int pageSize = 10)
        {
            var result = await _repository.GetTrainingSessionsPagedAsync(page, pageSize);
            return Ok(result);
        }

    }
}
