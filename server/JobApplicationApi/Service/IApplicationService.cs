using JobApplicationApi.DTO;
using JobApplicationApi.Models;

namespace JobApplicationApi.Service
{
    public interface IApplicationService
    {
        Task<PaginatedDTO<JobApplication>> GetApplications(int page, int size, string sortBy, string order, string[] statuses);
        Task<JobApplication> GetApplication(int id);
        Task UpdateApplication(int id, JobApplication jobApplication);
        Task<JobApplication> AddApplication(JobApplication jobApplication);
        bool JobApplicationExists(int id);
    }
}
