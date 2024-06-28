using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_s24787.Services;

namespace Project_s24787.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncomesController : ControllerBase
{
    private readonly IDbService _dbService;

    public IncomesController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [Authorize]
    [HttpGet("real")]
    public async Task<IActionResult> GetRealIncome()
    {
        return Ok(await _dbService.GetRealIncome());
    }

    [Authorize]
    [HttpGet("expected")]
    public async Task<IActionResult> GetExpectedIncome()
    {
        return Ok(await _dbService.GetExpectedIncome());
    }
}