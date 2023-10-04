using AutoMapper;
using BudgetManagementApi.Models;
using BudgetManagementApi.Models.User;
using BudgetManagementApi.Repositories;
using BudgetManagementApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BudgetManagementApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserController(
            ILogger<UserController> logger,
            IUserRepository userRepository,
            IMapper mapper,
            ITokenService tokenService
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var userValidate = await _userRepository.LoginAsync(user);
            if (userValidate == null)
                return Unauthorized();
            var token = _tokenService.GenerateJwtToken(userValidate.UserId, userValidate.UserName);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var result = await _userRepository.RegisterAsync(user);
            if (result is null)
                return BadRequest("User could be not created.");
            return Ok();
        }
    }
}
