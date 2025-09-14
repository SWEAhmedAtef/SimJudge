using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimJudge.Application.DTOs;
using SimJudge.Domain.Interfaces.Services;

namespace SimJudge.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;
        private readonly IMapper _mapper;

        public SubmissionsController(ISubmissionService submissionService, IMapper mapper)
        {
            _submissionService = submissionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubmissionDto>>> GetSubmissions()
        {
            var submissions = await _submissionService.GetSubmissionsAsync();
            var submissionDtos = _mapper.Map<IEnumerable<SubmissionDto>>(submissions);
            return Ok(submissionDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubmissionDto>> GetSubmission(int id)
        {
            var submission = await _submissionService.GetSubmissionByIdAsync(id);
            if (submission == null)
                return NotFound();

            var submissionDto = _mapper.Map<SubmissionDto>(submission);
            return Ok(submissionDto);
        }

        [HttpPost]
        public async Task<ActionResult<SubmissionDto>> CreateSubmission([FromBody] CreateSubmissionDto createSubmissionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var submission = _mapper.Map<Domain.Entities.Submission>(createSubmissionDto);
            var createdSubmission = await _submissionService.CreateSubmissionAsync(submission);
            var createdSubmissionDto = _mapper.Map<SubmissionDto>(createdSubmission);

            return CreatedAtAction(nameof(GetSubmission), new { id = createdSubmission.Id }, createdSubmissionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubmission(int id, [FromBody] SubmissionDto submissionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var submission = await _submissionService.GetSubmissionByIdAsync(id);
            if (submission == null)
                return NotFound();

            _mapper.Map(submissionDto, submission);
            await _submissionService.UpdateSubmissionAsync(submission);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmission(int id)
        {
            var submission = await _submissionService.GetSubmissionByIdAsync(id);
            if (submission == null)
                return NotFound();

            await _submissionService.DeleteSubmissionAsync(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<SubmissionDto>>> GetUserSubmissions(int userId)
        {
            var submissions = await _submissionService.GetUserSubmissionsAsync(userId);
            var submissionDtos = _mapper.Map<IEnumerable<SubmissionDto>>(submissions);
            return Ok(submissionDtos);
        }

        [HttpGet("problem/{problemId}")]
        public async Task<ActionResult<IEnumerable<SubmissionDto>>> GetProblemSubmissions(int problemId)
        {
            var submissions = await _submissionService.GetProblemSubmissionsAsync(problemId);
            var submissionDtos = _mapper.Map<IEnumerable<SubmissionDto>>(submissions);
            return Ok(submissionDtos);
        }
    }
}
