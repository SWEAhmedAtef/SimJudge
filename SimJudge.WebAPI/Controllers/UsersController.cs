using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimJudge.Application.DTOs;
using SimJudge.Domain.Interfaces.Services;

namespace SimJudge.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet("username/{userName}")]
        public async Task<ActionResult<UserDto>> GetUserByUserName(string userName)
        {
            var user = await _userService.GetUserByUserNameAsync(userName);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<Domain.Entities.User>(createUserDto);
            var createdUser = await _userService.CreateUserAsync(user);
            var createdUserDto = _mapper.Map<UserDto>(createdUser);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUserDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            _mapper.Map(userDto, user);
            await _userService.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
