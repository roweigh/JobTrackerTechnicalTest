using JobApplicationApi.DTO;
using JobApplicationApi.Models;

namespace JobApplicationApi.Service
{
    public interface IApplicationService
    {
        Task<PaginationContentDTO<JobApplication>> GetApplications(int? page, int? size, string? sortBy, string? sortDesc, string? status);
        Task<JobApplication> GetApplication(int id);
        Task UpdateApplication(int id, JobApplication jobApplication);
        Task<JobApplication> AddApplication(JobApplication jobApplication);
        bool JobApplicationExists(int id);
    }
}
