using JobApplicationApi.Models;

namespace JobApplicationApi.Repositories
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<Application>> GetAll();
        Task<Application> Get(int id);
        Task Post(Application application);
        Task Put(int id, Application application);
        bool Exists(int id);
    }
}
