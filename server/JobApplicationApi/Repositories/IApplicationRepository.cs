using JobApplicationApi.Models;

namespace JobApplicationApi.Repositories
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAll();
        Task<JobApplication> Get(int id);
        Task Post(JobApplication jobApplication);
        Task Put(int id, JobApplication jobApplication);
        bool Exists(int id);
    }
}
