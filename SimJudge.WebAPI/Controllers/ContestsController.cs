using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimJudge.Application.DTOs;
using SimJudge.Domain.Interfaces.Services;

namespace SimJudge.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContestsController : ControllerBase
    {
        private readonly IContestService _contestService;
        private readonly IMapper _mapper;

        public ContestsController(IContestService contestService, IMapper mapper)
        {
            _contestService = contestService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContestDto>>> GetContests()
        {
            var contests = await _contestService.GetContestsAsync();
            var contestDtos = _mapper.Map<IEnumerable<ContestDto>>(contests);
            return Ok(contestDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContestDto>> GetContest(int id)
        {
            var contest = await _contestService.GetContestByIdAsync(id);
            if (contest == null)
                return NotFound();

            var contestDto = _mapper.Map<ContestDto>(contest);
            return Ok(contestDto);
        }

        [HttpPost]
        public async Task<ActionResult<ContestDto>> CreateContest([FromBody] CreateContestDto createContestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contest = _mapper.Map<Domain.Entities.Contest>(createContestDto);
            var createdContest = await _contestService.CreateContestAsync(contest);
            var createdContestDto = _mapper.Map<ContestDto>(createdContest);

            return CreatedAtAction(nameof(GetContest), new { id = createdContest.Id }, createdContestDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContest(int id, [FromBody] ContestDto contestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contest = await _contestService.GetContestByIdAsync(id);
            if (contest == null)
                return NotFound();

            _mapper.Map(contestDto, contest);
            await _contestService.UpdateContestAsync(contest);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContest(int id)
        {
            var contest = await _contestService.GetContestByIdAsync(id);
            if (contest == null)
                return NotFound();

            await _contestService.DeleteContestAsync(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ContestDto>>> GetUserContests(int userId)
        {
            var contests = await _contestService.GetUserContestsAsync(userId);
            var contestDtos = _mapper.Map<IEnumerable<ContestDto>>(contests);
            return Ok(contestDtos);
        }
    }
}
