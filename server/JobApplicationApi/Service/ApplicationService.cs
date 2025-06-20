using JobApplicationApi.DTO;
using JobApplicationApi.Models;
using JobApplicationApi.Repositories;

namespace JobApplicationApi.Service
{
    public class ApplicationService(IApplicationRepository repository) : IApplicationService
    {
        private readonly IApplicationRepository _repository = repository;

        public async Task<PaginatedDTO<JobApplication>> GetApplications(int page, int size, string sortBy, string order, string[] statuses)
        {
            var applications = await _repository.GetAll(page, size, sortBy, order, statuses);
            var totalElements = await _repository.Count(statuses);
            var totalPages = (int)Math.Ceiling((double)totalElements / size);

            return new PaginatedDTO<JobApplication>
            {
                content = applications,
                pagination = new PaginationDTO
                {
                    page = page,
                    size = size,
                    totalElements = totalElements,
                    totalPages = totalPages,
                    first = page == 1,
                    last = page >= totalPages
                }
            };
        }
        public async Task<JobApplication> GetApplication(int id)
        {
            return await _repository.Get(id);
        }

        public async Task UpdateApplication(int id, JobApplication jobApplication)
        {
            await _repository.Put(id, jobApplication);
        }

        public async Task<JobApplication> AddApplication(JobApplication jobApplication)
        {
            await _repository.Post(jobApplication);
            return jobApplication;
        }

        public bool JobApplicationExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}
