using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.DTOs.GenreDtos;
using MovieApp.BL.Services.Interfaces;
using MovieApp.BL.Utilities.Enums;

namespace MovieApp.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class GenresController : ControllerBase
{
    readonly IGenreService _service;
    public GenresController(IGenreService service)
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
    public async Task<IActionResult> Create(GenreCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpPost]
    public async Task<IActionResult> CreateRange(IEnumerable<GenreCreateDto> dtos)
    {
        await _service.CreateRangeAsync(dtos); 
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GenreUpdateDto dto)
    {
        return Ok(await _service.UpdateAsync(id, dto));
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
