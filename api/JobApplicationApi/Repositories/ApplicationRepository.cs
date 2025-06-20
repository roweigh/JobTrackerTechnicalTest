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

        public async Task<IEnumerable<JobApplication>> GetAll()
        {
            IQueryable<JobApplication> items = _context.JobApplication;
            return await Task.FromResult(items);
        }

        public async Task<JobApplication> Get(int id)
        {
            return await _context.JobApplication.FindAsync(id);
        }

        public async Task Post(JobApplication jobApplication)
        {
            _context.JobApplication.Add(jobApplication);
            await _context.SaveChangesAsync();
        }

        public async Task Put(int id, JobApplication jobApplication)
        {
            _context.Entry(jobApplication).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.JobApplication.Any(e => e.id == id);
        }
    }
}
