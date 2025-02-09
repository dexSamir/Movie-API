using Microsoft.AspNetCore.Mvc;
using MovieApp.BL.DTOs.LanguageDtos;
using MovieApp.BL.Services.Interfaces;

[Route("api/[controller]/[action]")]
[ApiController]
public class LanguagesController : ControllerBase
{
    readonly ILanguageService _service;
    public LanguagesController(ILanguageService service)
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

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        return Ok(await _service.GetByCodeAsync(code));
    }

    [HttpPost]
    public async Task<IActionResult> Create(LanguageCreateDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, LanguageUpdateDto dto)
    {
        return Ok(await _service.UpdateAsync(dto, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _service.DeleteAsync(id));
    }

    [HttpDelete("code/{code}")]
    public async Task<IActionResult> DeleteByCode(string code)
    {
        return Ok(await _service.DeleteAsync(code));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        return Ok(await _service.SoftDeleteAsync(id));
    }

    [HttpDelete("code/{code}")]
    public async Task<IActionResult> SoftDeleteByCode(string code)
    {
        return Ok(await _service.SoftDeleteAsync(code));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ReverseSoftDelete(int id)
    {
        return Ok(await _service.ReverseDeleteAsync(id));
    }

    [HttpDelete("code/{code}")]
    public async Task<IActionResult> ReverseSoftDeleteByCode(string code)
    {
        return Ok(await _service.ReverseDeleteAsync(code));
    }

    [HttpDelete("{ids}")]
    public async Task<IActionResult> DeleteRange([FromRoute] string ids)
    {
        return Ok(await _service.DeleteRangeAsync(ids));
    }

    [HttpDelete("{ids}")]
    public async Task<IActionResult> SoftDeleteRange([FromRoute] string ids)
    {
        return Ok(await _service.SoftDeleteRangeAsync(ids));
    }

    [HttpDelete("{ids}")]
    public async Task<IActionResult> ReverseSoftDeleteRange([FromRoute] string ids)
    {
        return Ok(await _service.ReverseDeleteRangeAsync(ids));
    }
}
