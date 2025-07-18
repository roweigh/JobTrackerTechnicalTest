﻿using JobApplicationApi.DTO;
using JobApplicationApi.Models;
using JobApplicationApi.Repositories;

namespace JobApplicationApi.Service
{
    public class ApplicationService(IApplicationRepository repository) : IApplicationService
    {
        private readonly IApplicationRepository _repository = repository;

        public async Task<PaginationContentDTO<ApplicationDTO>> GetApplications(int? page, int? size, string? sortBy, string? sortDesc, string? status)
        {
            // Generate default values
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            string key = sortBy ?? "dateApplied";
            string order = sortDesc == "true" ? "desc" : "asc";
            string[] statuses = string.IsNullOrEmpty(status) ? [] : status.Split(',');
            var applications = await _repository.GetAll();

            // Filter items
            if (statuses.Any())
            {
                applications = applications.Where(item => statuses.Contains(item.status));
            }

            //  Generate pagination data after filtering
            var totalElements = applications.Count();
            var totalPages = (int)Math.Ceiling((double)totalElements / pageSize);
            var first = pageNumber == 1;
            var last = pageNumber >= totalPages;

            // Order items
            switch (key)
            {
                case "companyName":
                    applications = order == "desc" ? applications.OrderByDescending(item => item.companyName) : applications.OrderBy(item => item.companyName);
                    break;
                case "position":
                    applications = order == "desc" ? applications.OrderByDescending(item => item.position) : applications.OrderBy(item => item.position);
                    break;
                case "status":
                    applications = order == "desc" ? applications.OrderByDescending(item => item.status) : applications.OrderBy(item => item.status);
                    break;
                case "dateApplied":
                    applications = order == "desc" ? applications.OrderByDescending(item => item.dateApplied) : applications.OrderBy(item => item.dateApplied);
                    break;
                default:
                    applications = applications.OrderByDescending(item => item.dateApplied);
                    break;
            }

            var content = applications
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ApplicationMapper.ToDTO)
                .ToList();

            var pagination = new PaginationDTO
            {
                page = pageNumber,
                size = pageSize,
                totalElements = totalElements,
                totalPages = totalPages,
                first = first,
                last = last
            };

            return new PaginationContentDTO<ApplicationDTO>
            {
                content = content,
                pagination = pagination
            };
        }

        public async Task<ApplicationDTO> GetApplication(int id)
        {
            var application = await _repository.Get(id);
            return ApplicationMapper.ToDTO(application);
        }

        public async Task UpdateApplication(int id, ApplicationDTO dto)
        {
            var entity = ApplicationMapper.ToEntity(dto);
            await _repository.Put(id, entity);
        }

        public async Task<Application> AddApplication(ApplicationDTO dto)
        {
            var entity = ApplicationMapper.ToEntity(dto);
            await _repository.Post(entity);
            return entity;
        }

        public bool JobApplicationExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}
