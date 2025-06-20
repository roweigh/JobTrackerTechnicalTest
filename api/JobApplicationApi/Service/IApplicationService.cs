using JobApplicationApi.DTO;
using JobApplicationApi.Models;

namespace JobApplicationApi.Service
{
    public interface IApplicationService
    {
        Task<PaginationContentDTO<ApplicationDTO>> GetApplications(int? page, int? size, string? sortBy, string? sortDesc, string? status);
        Task<ApplicationDTO> GetApplication(int id);
        Task UpdateApplication(int id, ApplicationDTO application);
        Task<Application> AddApplication(ApplicationDTO application);
        bool JobApplicationExists(int id);
    }
}
