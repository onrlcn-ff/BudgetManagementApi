using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class IncomeController : ControllerBase
    {
        private readonly ILogger<IncomeController> _logger;
        private readonly IGenericRepository<Income> _incomeRepository;
        private readonly IMapper _mapper;

        public IncomeController(
            IGenericRepository<Income> incomeRepository,
            ILogger<IncomeController> logger,
            IMapper mapper
        )
        {
            _incomeRepository = incomeRepository;
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

            IEnumerable<Income> incomes = await _incomeRepository.GetAllAsync(userId);
            List<IncomeDto> incomeDtos = new List<IncomeDto>();

            incomes.ToList().ForEach(i => incomeDtos.Add(_mapper.Map<IncomeDto>(i)));

            return Ok(incomeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetIncomeById(int id)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            var income = await _incomeRepository.GetById(id);
            if (income is null)
                return NotFound();

            var incomeDto = _mapper.Map<IncomeDto>(income);

            return Ok(incomeDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] IncomeDto incomeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            var income = new Income
            {
                UserId = userId,
                Amount = incomeDto.Amount,
                Description = incomeDto.Description,
                Date = DateTime.Now
            };

            await _incomeRepository.InsertAsync(income);
            return CreatedAtAction(nameof(CreateIncome), new { id = income.Id }, incomeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] IncomeDto incomeDto)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            Income income = _mapper.Map<Income>(incomeDto);
            income.Id = id;
            income.Date = DateTime.Now;
            income.UserId = userId;
            await _incomeRepository.UpdateAsync(income);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeById(int id)
        {
            await _incomeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
