using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationApi.DTO;
using JobApplicationApi.Models;
using JobApplicationApi.Service;

namespace JobApplicationApi.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _service;

        public ApplicationsController(IApplicationService service)
        {
            _service = service;
        }

        // GET: api/applications
        [HttpGet]
        public async Task<ActionResult<PaginationContentDTO<JobApplication>>> GetApplications(int? page, int? size, string? sortBy, string? sortDesc, string? status)
        {
            var response = await _service.GetApplications(page, size, sortBy, sortDesc, status);
            return Ok(response);
        }

        // GET: api/applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplicationDTO>> GetApplication(int id)
        {
            var jobApplication = await _service.GetApplication(id);

            if (jobApplication == null)
            {
                return NotFound();
            }

            return jobApplication;
        }

        // PUT: api/applications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, JobApplication jobApplication)
        {
            if (id != jobApplication.id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateApplication(id, JobApplicationMapper.ToDTO(jobApplication));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.JobApplicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/applications
        [HttpPost]
        public async Task<ActionResult<JobApplication>> AddApplication(JobApplication jobApplication)
        {
            var newApplication = await _service.AddApplication(JobApplicationMapper.ToDTO(jobApplication));
            return CreatedAtAction(nameof(GetApplication), new { id = newApplication.id }, newApplication);
        }
    }
}
