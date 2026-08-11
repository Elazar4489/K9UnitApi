//using System.ComponentModel.DataAnnotations;

//namespace KUnitApi.DTOs;

//public class CreateDogDto
//{
//    [Required]
//    [StringLength(50)]
//    public string Name { get; set; } = string.Empty;

//    [Required]
//    [StringLength(50)]
//    public string Breed { get; set; } = string.Empty;

//    [Required]
//    [StringLength(15)]
//    public string MicrochipId { get; set; } = string.Empty;

//    [Required]
//    public DateTime DateOfBirth { get; set; }

//    [Required]
//    public string Specialty { get; set; } = string.Empty;

//    public string? Status { get; set; }
//}

//public class DogResponseDto
//{
//    public int Id { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public string Breed { get; set; } = string.Empty;
//    public string MicrochipId { get; set; } = string.Empty;
//    public DateTime DateOfBirth { get; set; }
//    public string Specialty { get; set; } = string.Empty;
//    public string Status { get; set; } = string.Empty;
//}





//public interface IDogRepository
//{
//    Task<bool> MicrochipExistsAsync(string microchipId);
//    Task<Dog> CreateDogAsync(Dog dog);
//}



//using Microsoft.EntityFrameworkCore;
//using KUnitApi.Data;
//using KUnitApi.Models;

//namespace KUnitApi.Repositories;

//public class DogRepository : IDogRepository
//{
//    private readonly AppDbContext _context;

//    public DogRepository(AppDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<bool> MicrochipExistsAsync(string microchipId)
//    {
//        return await _context.Dogs.AnyAsync(d => d.MicrochipId == microchipId);
//    }

//    public async Task<Dog> CreateDogAsync(Dog dog)
//    {
//        await _context.Dogs.AddAsync(dog);
//        await _context.SaveChangesAsync();
//        return dog;
//    }
//}




//using Microsoft.AspNetCore.Mvc;
//using KUnitApi.DTOs;
//using KUnitApi.Models;
//using KUnitApi.Repositories;

//namespace KUnitApi.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class DogsController : ControllerBase
//{
//    private readonly IDogRepository _dogRepository;

//    private static readonly HashSet<string> AllowedSpecialties = new(StringComparer.OrdinalIgnoreCase)
//    {
//        "Explosive Detection", "Narcotics Detection", "Tracking", "Attack", "Search"
//    };

//    private static readonly HashSet<string> AllowedStatuses = new(StringComparer.OrdinalIgnoreCase)
//    {
//        "Active", "In Training", "Retired"
//    };

//    public DogsController(IDogRepository dogRepository)
//    {
//        _dogRepository = dogRepository;
//    }

//    [HttpPost]
//    public async Task<IActionResult> CreateDog([FromBody] CreateDogDto dto)
//    {


//        var dog = new Dog
//        {
//            Name = dto.Name,
//            Breed = dto.Breed,
//            MicrochipId = dto.MicrochipId,
//            DateOfBirth = dto.DateOfBirth,
//            Specialty = dto.Specialty,
//            Status = finalStatus
//        };

//        var createdDog = await _dogRepository.CreateDogAsync(dog);

//        var responseDto = new DogResponseDto
//        {
//            Id = createdDog.Id,
//            Name = createdDog.Name,
//            Breed = createdDog.Breed,
//            MicrochipId = createdDog.MicrochipId,
//            DateOfBirth = createdDog.DateOfBirth,
//            Specialty = createdDog.Specialty,
//            Status = createdDog.Status
//        };

//        return CreatedAtAction(nameof(GetDogById), new { id = responseDto.Id }, responseDto);
//    }

//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetDogById(int id)
//    {
//        return Ok();
//    }
//}

//[HttpPost]
//    public IActionResult Create(PersonModel model)
//    {
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }

//        _repository.Save(model);
//        return Ok();
//    }












//using System;
//using System.ComponentModel.DataAnnotations;

//public class PastDateAttribute : ValidationAttribute
//{
//    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    {
//        if (value is DateTime dateTime)
//        {
//            if (dateTime.Date < DateTime.Today)
//            {
//                return ValidationResult.Success;
//            }

//            return new ValidationResult();
//        }

//        return ValidationResult.Success;
//    }
//}
//public class PersonModel
//{
//    [Required(ErrorMessage = "תאריך לידה הוא שדה חובה")]
//    [PastDate(ErrorMessage = "תאריך הלידה חייב להיות תאריך בעבר בלבד")]
//    public DateTime BirthDate { get; set; }
//}
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//public class EventModel : IValidatableObject
//{
//    [Required]
//    public DateTime EventDate { get; set; }

//    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
//    {
//        if (EventDate.Date >= DateTime.Today)
//        {
//            yield return new ValidationResult(
//                "התאריך חייב להיות בעבר (לא היום ולא בעתיד)",
//                new[] { nameof(EventDate) }
//            );
//        }
//    }
//}
















//[ApiController]
//[Route("api/[controller]")]
//public class UsersController : ControllerBase
//{
//    private readonly AppDbContext _context;

//    public UsersController(AppDbContext context)
//    {
//        _context = context;
//    }

//    // 1. READ (Get All / Get By Id)
//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var users = await _context.Users.ToListAsync();
//        return Ok(users);
//    }

//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetById(int id)
//    {
//        var user = await _context.Users.FindAsync(id);
//        if (user == null)
//        {
//            return NotFound(new { Message = "המשתמש לא נמצא" });
//        }

//        return Ok(user);
//    }

//    // 2. UPDATE (Put)
//    [HttpPut("{id}")]
//    public async Task<IActionResult> Update(int id, UserModel model)
//    {
//        // ה-Data Annotations בודקים כאן את תקינות הנתונים החדשים
//        if (!ModelState.IsValid)
//        {
//            return BadRequest(ModelState);
//        }

//        var existingUser = await _context.Users.FindAsync(id);
//        if (existingUser == null)
//        {
//            return NotFound(new { Message = "המשתמש לעדכון לא נמצא" });
//        }

//        // עדכון השדות
//        existingUser.Username = model.Username;
//        existingUser.Email = model.Email;
//        existingUser.BirthDate = model.BirthDate;

//        await _context.SaveChangesAsync();
//        return NoContent(); // 204 - העדכון הצליח ואין תוכן להחזיר
//    }

//    // 3. DELETE (Delete)
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> Delete(int id)
//    {
//        var user = await _context.Users.FindAsync(id);
//        if (user == null)
//        {
//            return NotFound(new { Message = "המשתמש למחיקה לא נמצא" });
//        }

//        _context.Users.Remove(user);
//        await _context.SaveChangesAsync();

//        return Ok(new { Message = "המשתמש שנמחק בהצלחה" });
//    }
//}
