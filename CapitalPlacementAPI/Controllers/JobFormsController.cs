using CapitalPlacementAPI.Model;
using CapitalPlacementAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobFormsController : ControllerBase
    {
        private readonly IJobRepository _jobFormRepository;

        public JobFormsController(IJobRepository jobFormRepository)
        {
            _jobFormRepository = jobFormRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobForm([FromBody] JobForm jobForm)
        {
            jobForm.Id = Guid.NewGuid().ToString();
            await _jobFormRepository.AddJobFormAsync(jobForm);
            return CreatedAtAction(nameof(GetJobForm), new { id = jobForm.Id }, jobForm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobForm(string id, [FromBody] JobForm jobForm)
        {
            var existingJobForm = await _jobFormRepository.GetJobFormAsync(id);
            if (existingJobForm == null)
            {
                return NotFound();
            }

            jobForm.Id = id;
            await _jobFormRepository.UpdateJobFormAsync(jobForm);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetJobForms()
        {
            var jobForms = await _jobFormRepository.GetJobFormsAsync();
            return Ok(jobForms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobForm(string id)
        {
            var jobForm = await _jobFormRepository.GetJobFormAsync(id);
            if (jobForm == null)
            {
                return NotFound();
            }

            return Ok(jobForm);
        }
    }
}
