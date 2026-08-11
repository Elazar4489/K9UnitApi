using K9UnitApi.Data;
using K9UnitApi.DTOs;
using K9UnitApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace K9UnitApi.Repositories
{
    public class K9UnitRepository : IK9UnitRepository
    {
        public K9UnitDbContext _context;
        public K9UnitRepository(K9UnitDbContext context)
        {
            _context = context;
        }
        public async Task<bool> MicrochipExistsAsync(string microchipId)
        {
            return await _context.Dogs.AnyAsync(d => d.MicrochipId == microchipId);
        }

        public async Task<Dog> CreateDogAsync(Dog dog)
        {
            await _context.Dogs.AddAsync(dog);
            if (await MicrochipExistsAsync(dog.MicrochipId))
            {
                return null;
            }
            await _context.SaveChangesAsync();
            return dog;
        }

        public async Task<Dog> GetDogById(int id)
        {
            var theDog = await _context.Dogs.FindAsync(id);
            if (theDog is null)
            {
                return null;
            }
            return theDog;
        }

        public async Task<TrainingSession> CreateTrainingSessionAsync(TrainingSession trainingSession)
        {
            await _context.TrainingSessions.AddAsync(trainingSession);
            
            await _context.SaveChangesAsync();
            return trainingSession;
        }

        public async Task<bool> Delete(int id)
        {
            var handler = await _context.Handlers.FindAsync(id);
            if (handler == null)
            {
                return false;
            }

            _context.Handlers.Remove(handler);
            await _context.SaveChangesAsync();

            return true;
        }











        //=============================================================================================
        //====================================LINQ Queries===================================

        // 1
        public async Task<IEnumerable<SearchForDogsByFiltersDto>> SearchForDogsByFiltersAsync(string? specialty, string? status)
        {
            var query = _context.Dogs.AsQueryable();
            if (!string.IsNullOrWhiteSpace(specialty))
            {
                query = query.Where(d => d.Specialty == specialty);
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(d => d.Status == status);
            }
            var result = query.Select(d => new SearchForDogsByFiltersDto
            {
                Id = d.Id,
                Name = d.Name,
                Breed = d.Breed,
                Specialty = d.Specialty,
                Status = d.Status
            });
            return result;
        }

        // 2
        public async Task<IEnumerable<DogsWithHandlerDetailsDto>> GetDogsWithHandlerDetailsAsync()
        {
            var query = _context.Dogs.AsQueryable();
            var result = query.Select(d => new DogsWithHandlerDetailsDto
            {
                Id = d.Id,
                Name = d.Name,
                Breed = d.Breed,
                Specialty = d.Specialty,
                FullName = d.Handler.FullName,
                Rank = d.Handler.Rank,
                Status = d.Status
            });
            return result;
        }

        // 3
        public async Task<IEnumerable<PerformanceSummaryDto>> GetPerformanceSummaryAsync()
        {
            var query = _context.Dogs.AsQueryable();
            var result = query.Select(d => new PerformanceSummaryDto
            {
                Id = d.Id, 
                Name = d.Name,
                Specialty = d.Specialty,
                TrainingNum = d.TrainingSessions.Count,
                Avarage = d.TrainingSessions.Average(t=>t.PerformanceScore)
            });
            return result;
        }

        // 4
        public async Task<IEnumerable<TrainingSessionsWithDetailed>> GetTrainingSessionsWithDetailedAsync()
        {
            var query = _context.TrainingSessions.AsQueryable();
            var result = query.Select(t => new TrainingSessionsWithDetailed
            {
                Id = t.Id,
                SessionDate = t.SessionDate,
                DurationMinutes = t.DurationMinutes,
                TrainingType = t.TrainingType,
                PerformanceScore = t.PerformanceScore,
                Passed = t.Passed,
                Evaluator = t.Evaluator,
                Name = t.Dog.Name,
                Specialty = t.Dog.Specialty,
                FullName = t.Dog.Handler.FullName
            });
            return result;
        }

        // 5
        public async Task<TrainingSessionsPagedDto<TrainingSessionsItemDto>> GetTrainingSessionsPagedAsync(int page, int pageSize)
        {
            var query = _context.TrainingSessions.AsQueryable();
            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = await query.
                OrderByDescending(t => t.SessionDate).
                Skip((page - 1) * pageSize).
                Take(pageSize).
                Select(t => new TrainingSessionsItemDto
                {
                    Id = t.Id,
                    SessionDate = t.SessionDate,
                    PerformanceScore = t.PerformanceScore,
                    DogName = t.Dog.Name
                }).
                ToListAsync();
            var result = new TrainingSessionsPagedDto<TrainingSessionsItemDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };
            return result;
        } 
        





    }
}
