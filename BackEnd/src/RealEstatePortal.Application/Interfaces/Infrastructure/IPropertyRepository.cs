using RealStatePortal.Application.DTOs;
using RealStatePortal.Application.DTOs.Request;
using RealStatePortal.Application.DTOs.Response;

namespace RealStatePortal.Application.Interfaces.Infrastructure;

public interface IPropertyRepository
{
    Task<PaginationDto<SearchPropertyResponse>> Search(SearchPropertyRequest request);
    Task<PropertyDetailResponse> GetPropertyDetail(string propertyId);
}