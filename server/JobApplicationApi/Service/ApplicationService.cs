using JobApplicationApi.DTO;
using JobApplicationApi.Models;
using JobApplicationApi.Repositories;

namespace JobApplicationApi.Service
{
    public class ApplicationService(IApplicationRepository repository) : IApplicationService
    {
        private readonly IApplicationRepository _repository = repository;

        public async Task<PaginationContentDTO<JobApplication>> GetApplications(int? page, int? size, string? sortBy, string? sortDesc, string? status)
        {
            // Generate default values
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            string key = sortBy ?? "dateApplied";
            string order = sortDesc == "true" ? "desc" : "asc";
            string[] statuses = string.IsNullOrEmpty(status) ? [] : status.Split(',');

            // Compute pagination data
            var applications = await _repository.GetAll(pageNumber, pageSize, key, order, statuses);
            var totalElements = await _repository.Count(statuses);
            var totalPages = (int)Math.Ceiling((double)totalElements / pageSize);

            return new PaginationContentDTO<JobApplication>
            {
                content = applications,
                pagination = new PaginationDTO
                {
                    page = pageNumber,
                    size = pageSize,
                    totalElements = totalElements,
                    totalPages = totalPages,
                    first = pageNumber == 1,
                    last = pageNumber >= totalPages
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
