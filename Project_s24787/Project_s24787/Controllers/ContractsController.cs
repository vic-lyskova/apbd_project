using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_s24787.DTOs;
using Project_s24787.Services;

namespace Project_s24787.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractsController : ControllerBase
{
    private readonly IDbService _dbService;

    public ContractsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [Authorize]
    [HttpPost("{idClient}")]
    public async Task<IActionResult> DrawUpContract(int idClient, DrawUpContractDTO drawUpContractDto)
    {
        if (!await _dbService.DoesClientExist(idClient))
        {
            return NotFound("Client not found");
        }

        if (await _dbService.DoesClientHaveActiveContract(idClient, drawUpContractDto.Software))
        {
            return BadRequest("Client has active contract for the software");
        }
        
        if (drawUpContractDto.EndDate.Subtract(drawUpContractDto.StartDate).TotalDays < 3 ||
            drawUpContractDto.EndDate.Subtract(drawUpContractDto.StartDate).TotalDays > 30)
        {
            return BadRequest("The time interval does not match the regulations");
        }

        if (!await _dbService.DoesSoftwareExist(drawUpContractDto.Software))
        {
            return NotFound("Software not found");
        }

        await _dbService.DrawUpContract(idClient, drawUpContractDto);

        return Created();
    }

    [Authorize]
    [HttpPost("payments/{idClient}")]
    public async Task<IActionResult> AddPayment(int idClient, AddPaymentDTO addPaymentDto)
    {
        if (!await _dbService.DoesClientExist(idClient))
        {
            return NotFound("Client not found");
        }

        if (!await _dbService.DoesContractExist(addPaymentDto.IdContract))
        {
            return NotFound("Contract not found");
        }

        var contract = await _dbService.FindContract(addPaymentDto.IdContract);

        if (contract.EndDate < addPaymentDto.Date)
        {
            return BadRequest("Time limit for payment exceeded");
        }

        await _dbService.AddPayment(idClient, addPaymentDto);

        return Created();
    }
}