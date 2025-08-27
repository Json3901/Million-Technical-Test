using RealStatePortal.Application.DTOs;
using RealStatePortal.Application.DTOs.Request;
using RealStatePortal.Application.DTOs.Response;
using RealStatePortal.Application.Interfaces;
using RealStatePortal.Application.Interfaces.Infrastructure;

namespace RealStatePortal.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<PaginationDto<SearchPropertyResponse>> SearchProperties(SearchPropertyRequest request)
    {
        return await _propertyRepository.Search(request);
    }
    
    public async Task<PropertyDetailResponse> GetPropertyDetail(string propertyId)
    {
        return await _propertyRepository.GetPropertyDetail(propertyId);
    }
}