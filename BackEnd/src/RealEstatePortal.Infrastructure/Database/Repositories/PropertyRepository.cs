using MongoDB.Driver;
using RealEstatePortal.Domain.Entities;
using RealEstatePortal.Infrastructure.Database.Interfaces;
using RealStatePortal.Application.DTOs;
using RealStatePortal.Application.DTOs.Request;
using RealStatePortal.Application.DTOs.Response;
using RealStatePortal.Application.Interfaces.Infrastructure;

namespace RealEstatePortal.Infrastructure.Database.Repositories;

public class PropertyRepository : MongoRepository<Property>, IPropertyRepository
{
    private readonly IMongoRepository<Owner> _ownerRepository;
    private readonly IMongoRepository<PropertyImage> _propertyImageRepository;
    private readonly IMongoRepository<PropertyTrace> _propertyTraceRepository;

    public PropertyRepository(IMongoDatabase database, IMongoRepository<Owner> ownerRepository,
        IMongoRepository<PropertyImage> propertyImageRepository,
        IMongoRepository<PropertyTrace> propertyTraceRepository) : base(database)
    {
        _ownerRepository = ownerRepository;
        _propertyImageRepository = propertyImageRepository;
        _propertyTraceRepository = propertyTraceRepository;
    }

    public async Task<PaginationDto<SearchPropertyResponse>> Search(SearchPropertyRequest request)
    {
        var filter = Builders<Property>.Filter.Empty;
        filter = GetNamesFilterDefinition(request, filter);
        filter = GetAddressesFilterDefinition(request, filter);
        filter = GetPriceFilterDefinition(request, filter);
        filter = GetInternalCodesFilterDefinition(request, filter);
        filter = GetYearsFilterDefinition(request, filter);
        filter = await GetOwnerNamesFilterDefinition(request, filter);

        var sort = GetSortDefinition(request);
        var properties = await GetProperties(filter, sort, request.PageNumber, request.PageSize);

        if (properties.Count == 0)
            return new PaginationDto<SearchPropertyResponse>(request.PageNumber, request.PageSize, 0,
                new List<SearchPropertyResponse>());

        var ownerIds = properties.Select(p => p.OwnerId).ToList().Distinct();
        var owners = await _ownerRepository.Find("Id", ownerIds.ToList());
        var searchProperties = properties.Select(x => new SearchPropertyResponse
        {
            Id = x.Id,
            Name = x.Name,
            OwnerName = owners.FirstOrDefault(p => p.Id == x.OwnerId)?.Name ?? string.Empty,
            Address = x.Address,
            Price = x.Price,
            InternalCode = x.InternalCode,
            Year = x.Year
        }).ToList();

        var count = await Collection.CountDocumentsAsync(filter);

        return new PaginationDto<SearchPropertyResponse>(request.PageNumber, request.PageSize, count, searchProperties);
    }

    public async Task<PropertyDetailResponse> GetPropertyDetail(string propertyId)
    {
        var property = await Collection.Find(p => p.Id == propertyId).FirstOrDefaultAsync();
        if (property == null) return null;

        var owner = await _ownerRepository.Find(o => o.Id == property.OwnerId);
        var filterImages = Builders<PropertyImage>.Filter.Eq(x => x.PropertyId, property.Id);
        var images = await _propertyImageRepository.Find(filterImages);
        
        var filterTraces = Builders<PropertyTrace>.Filter.Eq(x => x.PropertyId, property.Id);
        var traces = await _propertyTraceRepository.Find(filterTraces);

        return new PropertyDetailResponse
        {
            Id = property.Id,
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            Year = property.Year,
            InternalCode = property.InternalCode,
            Owner = owner.Name,
            Images = images,
            Traces = traces
        };
    }

    private static FilterDefinition<Property> GetNamesFilterDefinition(SearchPropertyRequest request,
        FilterDefinition<Property> filter)
    {
        if (request.Names.Length == 0) return filter;
        var regexFilters = request.Names
            .Select(name =>
                Builders<Property>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(name, "i")));
        var namesFilter = Builders<Property>.Filter.Or(regexFilters);
        filter = Builders<Property>.Filter.And(filter, namesFilter);
        return filter;
    }

    private static FilterDefinition<Property> GetAddressesFilterDefinition(SearchPropertyRequest request,
        FilterDefinition<Property> filter)
    {
        if (request.Addresses.Length == 0) return filter;
        var regexFilters = request.Addresses
            .Select(address =>
                Builders<Property>.Filter.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(address, "i")));
        var addressesFilter = Builders<Property>.Filter.Or(regexFilters);
        filter = Builders<Property>.Filter.And(filter, addressesFilter);
        return filter;
    }

    private static FilterDefinition<Property> GetPriceFilterDefinition(SearchPropertyRequest request,
        FilterDefinition<Property> filter)
    {
        if (request is { MinPrice: decimal.MinValue, MaxPrice: decimal.MaxValue }) return filter;

        var priceFilter = Builders<Property>.Filter.Gte(p => p.Price, request.MinPrice) &
                          Builders<Property>.Filter.Lte(p => p.Price, request.MaxPrice);
        filter = Builders<Property>.Filter.And(filter, priceFilter);

        return filter;
    }

    private static FilterDefinition<Property> GetInternalCodesFilterDefinition(SearchPropertyRequest request,
        FilterDefinition<Property> filter)
    {
        if (request.InternalCodes.Length == 0) return filter;
        var regexFilters = request.InternalCodes
            .Select(code =>
                Builders<Property>.Filter.Regex(p => p.InternalCode,
                    new MongoDB.Bson.BsonRegularExpression(code, "i")));
        var internalCodesFilter = Builders<Property>.Filter.Or(regexFilters);
        filter = Builders<Property>.Filter.And(filter, internalCodesFilter);
        return filter;
    }

    private static FilterDefinition<Property> GetYearsFilterDefinition(SearchPropertyRequest request,
        FilterDefinition<Property> filter)
    {
        if (request.Years.Length == 0) return filter;
        var regexFilters = request.Years
            .Select(year => Builders<Property>.Filter.Regex(p => p.Year.ToString(),
                new MongoDB.Bson.BsonRegularExpression(year.ToString(), "i")));
        var yearsFilter = Builders<Property>.Filter.Or(regexFilters);
        filter = Builders<Property>.Filter.And(filter, yearsFilter);
        return filter;
    }

    private async Task<FilterDefinition<Property>> GetOwnerNamesFilterDefinition(SearchPropertyRequest request,
        FilterDefinition<Property> filter)
    {
        if (request.OwnerNames.Length == 0) return filter;

        var regexFilters = request.OwnerNames
            .Select(name =>
                Builders<Owner>.Filter.Regex(o => o.Name, new MongoDB.Bson.BsonRegularExpression(name, "i")));
        var ownersFilter = Builders<Owner>.Filter.Or(regexFilters);
        var owners = await _ownerRepository.Find(ownersFilter);
        var ownerIds = owners.Select(o => o.Id).ToList();

        if (ownerIds.Count == 0) return filter;

        var ownerNamesFilter = Builders<Property>.Filter.In(p => p.OwnerId, ownerIds);
        filter = Builders<Property>.Filter.And(filter, ownerNamesFilter);

        return filter;
    }

    private static SortDefinition<Property> GetSortDefinition(SearchPropertyRequest request)
    {
        SortDefinition<Property> sort = null;
        if (!string.IsNullOrEmpty(request.SortBy))
        {
            sort = request.OrderDesc
                ? Builders<Property>.Sort.Descending(request.SortBy)
                : Builders<Property>.Sort.Ascending(request.SortBy);
        }

        return sort;
    }

    private async Task<List<Property>> GetProperties(FilterDefinition<Property> filter, SortDefinition<Property> sort,
        int pageNumber, int pageSize)
    {
        var query = Collection.Find(filter);

        if (sort != null)
        {
            query = query.Sort(sort);
        }

        var properties = await query
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return properties;
    }
}