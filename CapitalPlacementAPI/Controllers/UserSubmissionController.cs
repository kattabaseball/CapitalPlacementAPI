using CapitalPlacementAPI.Model;
using CapitalPlacementAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubmissionController : ControllerBase
    {
        private readonly IUserSubmission _userSubmissionRepository;

        public UserSubmissionController(IUserSubmission userSubmissionRepository)
        {
            _userSubmissionRepository = userSubmissionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserSubmission([FromBody] UserSubmission userSubmission)
        {
            userSubmission.Id = Guid.NewGuid().ToString();
            await _userSubmissionRepository.AddUserSubmissionAsync(userSubmission);
            return CreatedAtAction(nameof(GetUserSubmission), new { id = userSubmission.Id }, userSubmission);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserSubmission(string id)
        {
            var userSubmission = await _userSubmissionRepository.GetUserSubmissionAsync(id);
            if (userSubmission == null)
            {
                return NotFound();
            }

            return Ok(userSubmission);
        }

        [HttpGet("job/{jobFormId}")]
        public async Task<IActionResult> GetUserSubmissions(string jobFormId)
        {
            var userSubmissions = await _userSubmissionRepository.GetUserSubmissionsAsync(jobFormId);
            return Ok(userSubmissions);
        }
    }
}
