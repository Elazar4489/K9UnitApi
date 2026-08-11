using K9UnitApi.DTOs;
using K9UnitApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace K9UnitApi.Repositories
{
    public interface IK9UnitRepository
    {
        Task<bool> MicrochipExistsAsync(string microchipId);
        Task<Dog> CreateDogAsync(Dog dog);
        Task<Dog> GetDogById(int id);
        Task<TrainingSession> CreateTrainingSessionAsync(TrainingSession trainingSession);
        Task<bool> Delete(int id);
        Task<IEnumerable<SearchForDogsByFiltersDto>> SearchForDogsByFiltersAsync(string? specialty, string? status);
        Task<IEnumerable<DogsWithHandlerDetailsDto>> GetDogsWithHandlerDetailsAsync();
        Task<IEnumerable<PerformanceSummaryDto>> GetPerformanceSummaryAsync();
        Task<IEnumerable<TrainingSessionsWithDetailed>> GetTrainingSessionsWithDetailedAsync();
        Task<TrainingSessionsPagedDto<TrainingSessionsItemDto>> GetTrainingSessionsPagedAsync(int page, int pageSize);
    }
}
