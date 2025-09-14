using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimJudge.Application.DTOs;
using SimJudge.Domain.Interfaces.Services;

namespace SimJudge.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProblemsController : ControllerBase
    {
        private readonly IProblemService _problemService;
        private readonly IMapper _mapper;

        public ProblemsController(IProblemService problemService, IMapper mapper)
        {
            _problemService = problemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProblemDto>>> GetProblems([FromQuery] string? query = null)
        {
            var problems = await _problemService.GetProblemsAsync(query);
            var problemDtos = _mapper.Map<IEnumerable<ProblemDto>>(problems);
            return Ok(problemDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemDto>> GetProblem(int id)
        {
            var problem = await _problemService.GetProblemByIdAsync(id);
            var problemDto = _mapper.Map<ProblemDto>(problem);
            return Ok(problemDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProblemDto>> CreateProblem([FromBody] CreateProblemDto createProblemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var problem = _mapper.Map<Domain.Entities.Problem>(createProblemDto);
            var createdProblem = await _problemService.CreateProblemAsync(problem);
            var createdProblemDto = _mapper.Map<ProblemDto>(createdProblem);

            return CreatedAtAction(nameof(GetProblem), new { id = createdProblem.Id }, createdProblemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProblem(int id, [FromBody] ProblemDto problemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var problem = await _problemService.GetProblemByIdAsync(id);
            _mapper.Map(problemDto, problem);
            await _problemService.UpdateProblemAsync(problem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProblem(int id)
        {
            await _problemService.DeleteProblemAsync(id);
            return NoContent();
        }
    }
}
