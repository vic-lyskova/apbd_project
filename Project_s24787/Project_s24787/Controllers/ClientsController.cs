using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_s24787.DTOs;
using Project_s24787.Services;

namespace Project_s24787.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IDbService _dbService;

    public ClientsController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [Authorize]
    [HttpPost("firms")]
    public async Task<IActionResult> AddFirmClient(AddFirmClientDTO addFirmClientDto)
    {
        if (await _dbService.DoesFirmClientExist(addFirmClientDto.KRSNumber))
        {
            return BadRequest($"Firm client with KRS Number {addFirmClientDto.KRSNumber} already in the system");
        }

        await _dbService.AddFirmClient(addFirmClientDto);
        return Created();
    }

    [Authorize]
    [HttpPost("individuals")]
    public async Task<IActionResult> AddIndividualClient(AddIndividualClientDTO addIndividualClientDto)
    {
        if (await _dbService.DoesIndividualClientExist(addIndividualClientDto.PESEL))
        {
            return BadRequest($"Client with PESEL {addIndividualClientDto.PESEL} already in the system");
        }

        await _dbService.AddIndividualClient(addIndividualClientDto);
        return Created();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("firms")]
    public async Task<IActionResult> RemoveIndividualClient()
    {
        return NoContent();
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("individuals")]
    public async Task<IActionResult> RemoveFirmClient()
    {
        return NoContent();
    }

    [Authorize(Roles = "admin")]
    [HttpPut("firms/{krsNumber}")]
    public async Task<IActionResult> EditFirmClient(string krsNumber, EditFirmClientDTO editFirmClientDto)
    {
        if (!await _dbService.DoesFirmClientExist(krsNumber))
        {
            return NotFound($"Firm client with KRS number {krsNumber} not found");
        }

        await _dbService.EditFirmClient(krsNumber, editFirmClientDto);
        
        return NoContent();
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("individuals/{pesel}")]
    public async Task<IActionResult> EditIndividualClient(string pesel, EditIndividualClientDTO editIndividualClientDto)
    {
        if (!await _dbService.DoesIndividualClientExist(pesel))
        {
            return NotFound($"Individual client with pesel {pesel} not found");
        }

        await _dbService.EditIndividualClient(pesel, editIndividualClientDto);
        
        return NoContent();
    }
}