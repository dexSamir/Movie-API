using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.DTOs.GenreDtos;
using MovieApp.BL.Services.Interfaces;

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
    public async Task<IActionResult> CreateRange(params GenreCreateDto[] dtos)
    {
        await _service.CreateRangeAsync(dtos); 
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _service.DeleteAsync(id));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        return Ok(await _service.SoftDeleteAsync(id));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> ReverseSoftDelete(int id)
    {
        return Ok(await _service.ReverseDeleteAsync(id));
    }
    [HttpDelete("{ids}")]
    public async Task<IActionResult> DeleteRange(params int[] ids)
    {
        return Ok(await _service.DeleteRangeAsync(ids));
    }
    [HttpDelete("{ids}")]
    public async Task<IActionResult> SoftDeleteRangeAsync(params int[] ids)
    {
        return Ok(await _service.SoftDeleteRangeAsync(ids));
    }
}
