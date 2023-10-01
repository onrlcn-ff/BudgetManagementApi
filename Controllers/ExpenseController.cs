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
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IGenericRepository<Expense> _expenseRepository;
        private readonly IMapper _mapper;

        public ExpenseController(
            IGenericRepository<Expense> expenseRepository,
            ILogger<ExpenseController> logger,
            IMapper mapper
        )
        {
            _expenseRepository = expenseRepository;
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

            IEnumerable<Expense> expenses = await _expenseRepository.GetAllAsync(userId);
            List<ExpenseDto> expenseDtos = new List<ExpenseDto>();

            expenses.ToList().ForEach(i => expenseDtos.Add(_mapper.Map<ExpenseDto>(i)));

            return Ok(expenseDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetIncomeById(int id)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            var expense = await _expenseRepository.GetById(id, userId);
            if (expense is null)
                return NotFound();

            var expenseDto = _mapper.Map<ExpenseDto>(expense);

            return Ok(expenseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] ExpenseDto expenseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            var expense = new Expense
            {
                UserId = userId,
                Amount = expenseDto.Amount,
                Description = expenseDto.Description,
                Date = expenseDto.Date
            };

            await _expenseRepository.InsertAsync(expense);
            return CreatedAtAction(nameof(CreateIncome), new { id = expense.Id }, expenseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] ExpenseDto expenseDto)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            Expense expense = _mapper.Map<Expense>(expenseDto);
            expense.Id = id;
            expense.Date = expenseDto.Date;
            expense.UserId = userId;
            await _expenseRepository.UpdateAsync(expense);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeById(int id)
        {
            await _expenseRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
