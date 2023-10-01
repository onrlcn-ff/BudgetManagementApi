using System.Runtime.InteropServices;
using System.Security.Claims;
using AutoMapper;
using BudgetManagementApi.Models;
using BudgetManagementApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [Authorize]
    public class SavingController : ControllerBase
    {
        private readonly ILogger<SavingController> _logger;
        private readonly IGenericRepository<Saving> _savingRepository;
        private readonly IMapper _mapper;

        public SavingController(
            IGenericRepository<Saving> savingRepository,
            ILogger<SavingController> logger,
            IMapper mapper
        )
        {
            _savingRepository = savingRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            IEnumerable<Saving> savings = await _savingRepository.GetAllAsync(userId);
            List<SavingDto> savingDtos = new List<SavingDto>();

            savings.ToList().ForEach(i => savingDtos.Add(_mapper.Map<SavingDto>(i)));

            return Ok(savingDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetIncomeById(int id)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            var saving = await _savingRepository.GetById(id, userId);
            if (saving is null)
                return NotFound();

            var savingDto = _mapper.Map<ExpenseDto>(saving);

            return Ok(saving);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] SavingDto savingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            var saving = new Saving
            {
                UserId = userId,
                Amount = savingDto.Amount,
                Goal = savingDto.Goal
            };

            await _savingRepository.InsertAsync(saving);
            return CreatedAtAction(nameof(CreateIncome), new { id = saving.Id }, saving);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] SavingDto savingDto)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            Saving saving = _mapper.Map<Saving>(savingDto);
            saving.Id = id;
            saving.UserId = userId;
            await _savingRepository.UpdateAsync(saving);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeById(int id)
        {
            await _savingRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
