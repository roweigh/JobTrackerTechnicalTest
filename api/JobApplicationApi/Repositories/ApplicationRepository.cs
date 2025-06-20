using JobApplicationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationApi.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDBContext _context;

        public ApplicationRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Application>> GetAll()
        {
            IQueryable<Application> items = _context.JobApplication;
            return await Task.FromResult(items);
        }

        public async Task<Application> Get(int id)
        {
            return await _context.JobApplication.FindAsync(id);
        }

        public async Task Post(Application application)
        {
            _context.JobApplication.Add(application);
            await _context.SaveChangesAsync();
        }

        public async Task Put(int id, Application application)
        {
            _context.Entry(application).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.JobApplication.Any(e => e.id == id);
        }
    }
}
