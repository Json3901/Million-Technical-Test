using Moq;
using RealStatePortal.Application.DTOs;
using RealStatePortal.Application.DTOs.Request;
using RealStatePortal.Application.DTOs.Response;
using RealStatePortal.Application.Interfaces.Infrastructure;
using RealStatePortal.Application.Services;

namespace RealEstatePortal.Application.Tests.Services;

[TestFixture]
public class PropertyServiceTests
{
    private Mock<IPropertyRepository> _propertyRepositoryMock;
    private PropertyService _service;

    [SetUp]
    public void SetUp()
    {
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _service = new PropertyService(_propertyRepositoryMock.Object);
    }

    [Test]
    public async Task SearchProperties_ReturnsExpectedResult()
    {
        var request = new SearchPropertyRequest();
        var properties = new List<SearchPropertyResponse>
        {
            new() { Id = "1234567" },
            new() { Id = "12345678" }
        };
        var expected = new PaginationDto<SearchPropertyResponse>(
            request.PageNumber,
            request.PageSize,
            properties.Count,
            properties);
        _propertyRepositoryMock.Setup(r => r.Search(request)).ReturnsAsync(expected);

        var result = await _service.SearchProperties(request);

        Assert.That(result, Is.EqualTo(expected));
        _propertyRepositoryMock.Verify(r => r.Search(request), Times.Once);
    }

    [Test]
    public async Task GetPropertyDetail_ReturnsPropertyDetail()
    {
        var propertyId = "8765432";
        var expected = new PropertyDetailResponse { Id = propertyId };
        _propertyRepositoryMock.Setup(r => r.GetPropertyDetail(propertyId)).ReturnsAsync(expected);

        var result = await _service.GetPropertyDetail(propertyId);

        Assert.That(result, Is.EqualTo(expected));
        _propertyRepositoryMock.Verify(r => r.GetPropertyDetail(propertyId), Times.Once);
    }
}