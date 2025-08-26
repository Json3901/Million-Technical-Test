using Microsoft.AspNetCore.Mvc;
using RealStatePortal.Application.DTOs.Request;
using RealStatePortal.Application.Interfaces;

namespace RealStatePortal.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchPropertyRequest request)
    {
        var response = await _propertyService.SearchProperties(request);
        return Ok(response);
    }
}