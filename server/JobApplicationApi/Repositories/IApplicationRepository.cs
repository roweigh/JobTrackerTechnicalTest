using JobApplicationApi.Models;

namespace JobApplicationApi.Repositories
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAll(int page, int size, string sort, string order, string[] statuses);
        Task<JobApplication> Get(int id);
        Task Post(JobApplication jobApplication);
        Task Put(int id, JobApplication jobApplication);
        Task<int> Count(string[] statuses);
        bool Exists(int id);
    }
}
