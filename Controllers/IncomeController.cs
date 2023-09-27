using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BudgetManagementApi.Models;
using BudgetManagementApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly ILogger<IncomeController> _logger;
        private readonly IGenericRepository<Income> _incomeRepository;

        public IncomeController(
            IGenericRepository<Income> incomeRepository,
            ILogger<IncomeController> logger
        )
        {
            _incomeRepository = incomeRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            var incomes = await _incomeRepository.GetAllAsync();
            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeById(int id)
        {
            var income = await _incomeRepository.GetById(id);
            if (income is null)
                return BadRequest("Income Not Found");

            return Ok(income);
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
            return CreatedAtAction(nameof(GetIncomeById), new { id = income.IncomeId }, incomeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] Income income)
        {
            if (id != income.IncomeId)
                return BadRequest();
            await _incomeRepository.UpdateAsync(income);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeById(int id)
        {
            var income = await _incomeRepository.GetById(id);
            if (income is null)
                return NotFound();
            await _incomeRepository.DeleteAsync(income);
            return NoContent();
        }
    }
}
