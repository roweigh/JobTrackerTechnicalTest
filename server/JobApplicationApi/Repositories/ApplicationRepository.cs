using Microsoft.EntityFrameworkCore;
using JobApplicationApi.Models;

namespace JobApplicationApi.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDBContext _context;

        public ApplicationRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobApplication>> GetAll(int page, int size, string sort, string order, string[] statuses)
        {
            IQueryable<JobApplication> items = _context.JobApplication;
            
            if (statuses.Any())
            {
                items = items.Where(item => statuses.Contains(item.status));
            }

            switch (sort)
            {
                case "companyName":
                    items = order == "desc" ? items.OrderByDescending(item => item.companyName) : items.OrderBy(item => item.companyName);
                    break;
                case "position":
                    items = order == "desc" ? items.OrderByDescending(item => item.position) : items.OrderBy(item => item.position);
                    break;
                case "status":
                    items = order == "desc" ? items.OrderByDescending(item => item.status) : items.OrderBy(item => item.status);
                    break;
                case "dateApplied":
                    items = order == "desc" ? items.OrderByDescending(item => item.dateApplied) : items.OrderBy(item => item.dateApplied);
                    break;
                default:
                    items = items.OrderByDescending(item => item.dateApplied);
                    break;
            }

            return await items
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToListAsync();
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

        // Helper function used to count total elements in paginated data
        public Task<int> Count(string[] statuses)
        {
            if (statuses.Any())
            {
                return _context.JobApplication.Where(item => statuses.Contains(item.status)).CountAsync();
            } 
            else {
                return _context.JobApplication.CountAsync();
            }
        }

        public bool Exists(int id)
        {
            return _context.JobApplication.Any(e => e.id == id);
        }
    }
}
