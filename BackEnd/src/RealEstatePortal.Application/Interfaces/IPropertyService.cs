using RealStatePortal.Application.DTOs;
using RealStatePortal.Application.DTOs.Request;
using RealStatePortal.Application.DTOs.Response;

namespace RealStatePortal.Application.Interfaces;

public interface IPropertyService
{
    Task<PaginationDto<SearchPropertyResponse>> SearchProperties(SearchPropertyRequest request);
    Task<PropertyDetailResponse> GetPropertyDetail(string propertyId);
}