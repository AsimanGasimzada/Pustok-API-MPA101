using Microsoft.AspNetCore.Mvc;
using Pustok.Business.Dtos;
using Pustok.Business.Services.Abstractions;

namespace Pustok.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService _service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CategoryCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] CategoryUpdateDto dto)
    {
        var result = await _service.UpdateAsync(dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _service.DeleteAsync(id);
        return Ok(result);
    }
}
