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
    public class InvestmentController : ControllerBase
    {
        private readonly ILogger<InvestmentController> _logger;
        private readonly IGenericRepository<Investment> _investmentRepository;
        private readonly IMapper _mapper;

        public InvestmentController(
            IGenericRepository<Investment> investmenRepository,
            ILogger<InvestmentController> logger,
            IMapper mapper
        )
        {
            _investmentRepository = investmenRepository;
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

            IEnumerable<Investment> investments = await _investmentRepository.GetAllAsync(userId);
            List<InvestmentDto> investmentDtos = new List<InvestmentDto>();

            investments.ToList().ForEach(i => investmentDtos.Add(_mapper.Map<InvestmentDto>(i)));

            return Ok(investmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetIncomeById(int id)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            var investment = await _investmentRepository.GetById(id, userId);
            if (investment is null)
                return NotFound();

            var investmentDto = _mapper.Map<InvestmentDto>(investment);

            return Ok(investmentDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] Investment investmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            var investment = new Investment
            {
                UserId = userId,
                Amount = investmentDto.Amount,
                Type = investmentDto.Type,
                Date = investmentDto.Date
            };

            await _investmentRepository.InsertAsync(investment);
            return CreatedAtAction(nameof(CreateIncome), new { id = investment.Id }, investmentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(
            int id,
            [FromBody] InvestmentDto investmentDto
        )
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();
            int userId = int.Parse(userIdClaim.Value);

            Investment investment = _mapper.Map<Investment>(investmentDto);
            investment.Id = id;
            investment.UserId = userId;
            await _investmentRepository.UpdateAsync(investment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeById(int id)
        {
            await _investmentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
