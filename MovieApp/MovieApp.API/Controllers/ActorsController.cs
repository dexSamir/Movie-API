using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.DTOs.ActorDtos;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ActorsController : ControllerBase
{
    readonly IActorService _service;
    public ActorsController(IActorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Create(ActorCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, ActorUpdateDto dto)
    {
        return Ok(await _service.UpdateAsync(dto, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _service.DeleteAsync(id, EDeleteType.Hard));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(string id)
    {
        return Ok(await _service.DeleteAsync(id, EDeleteType.Soft));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> ReverseSoftDelete(string id)
    {
        return Ok(await _service.DeleteAsync(id, EDeleteType.Soft));
    }
    [HttpDelete("{ids}")]
    public async Task<IActionResult> DeleteRange([FromRoute] string ids)
    {
        return Ok(await _service.DeleteAsync(ids, EDeleteType.Soft));
    }
    [HttpDelete("{ids}")]
    public async Task<IActionResult> SoftDeleteRange([FromRoute] string ids)
    {
        return Ok(await _service.DeleteAsync(ids, EDeleteType.Soft));
    }
    [HttpDelete("{ids}")]
    public async Task<IActionResult> ReverseSoftDeleteRange([FromRoute] string ids)
    {
        return Ok(await _service.DeleteAsync(ids, EDeleteType.Soft));
    }
}
