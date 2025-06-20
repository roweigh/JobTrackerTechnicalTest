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
        public async Task<ActionResult<PaginationContentDTO<Application>>> GetApplications(int? page, int? size, string? sortBy, string? sortDesc, string? status)
        {
            var response = await _service.GetApplications(page, size, sortBy, sortDesc, status);
            return Ok(response);
        }

        // GET: api/applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDTO>> GetApplication(int id)
        {
            var application = await _service.GetApplication(id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }

        // PUT: api/applications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, Application application)
        {
            if (id != application.id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateApplication(id, ApplicationMapper.ToDTO(application));
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
        public async Task<ActionResult<Application>> AddApplication(Application application)
        {
            var dto = ApplicationMapper.ToDTO(application);
            var newApplication = await _service.AddApplication(dto);
            return CreatedAtAction(nameof(GetApplication), new { id = newApplication.id }, newApplication);
        }
    }
}
