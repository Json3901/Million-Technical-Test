using Microsoft.AspNetCore.Mvc;
using RealStatePortal.Application.DTOs.Request;
using RealStatePortal.Application.Interfaces;

namespace RealStatePortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet("{propertyId}")]
    public async Task<IActionResult> GetPropertyDetail(string propertyId)
    {
        var response = await _propertyService.GetPropertyDetail(propertyId);
        return Ok(response);
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchPropertyRequest request)
    {
        var response = await _propertyService.SearchProperties(request);
        return Ok(response);
    }
}