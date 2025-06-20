using JobApplicationApi.DTO;
using JobApplicationApi.Models;

namespace JobApplicationApi.Service
{
    public interface IApplicationService
    {
        Task<PaginationContentDTO<JobApplicationDTO>> GetApplications(int? page, int? size, string? sortBy, string? sortDesc, string? status);
        Task<JobApplicationDTO> GetApplication(int id);
        Task UpdateApplication(int id, JobApplicationDTO jobApplication);
        Task<JobApplication> AddApplication(JobApplicationDTO jobApplication);
        bool JobApplicationExists(int id);
    }
}
