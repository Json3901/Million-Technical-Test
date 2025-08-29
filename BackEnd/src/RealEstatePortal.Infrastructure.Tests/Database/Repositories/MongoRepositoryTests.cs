using Moq;
using MongoDB.Driver;
using System.Linq.Expressions;
using RealEstatePortal.Domain.Entities;
using RealEstatePortal.Infrastructure.Database.Repositories;
using RealEstatePortal.Domain.Attributes;
using System.Reflection;
using MongoDB.Bson.Serialization;
using NUnit.Framework.Internal;

[TestFixture]
public class MongoRepositoryTests
{
    private readonly Mock<IMongoDatabase> _dbMock;
    private readonly Mock<IMongoCollection<TestEntity>> _collectionMock;
    private readonly MongoRepository<TestEntity> _repo;
    private readonly IBsonSerializer<TestEntity> _serializer;

    public MongoRepositoryTests()
    {
        var cursor = new Mock<IAsyncCursor<TestEntity>>();
        cursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);

        cursor.Setup(x => x.Current)
            .Returns(new List<TestEntity>());

        _collectionMock = new Mock<IMongoCollection<TestEntity>>();
        _collectionMock.Setup(m => m.FindAsync(It.IsAny<FilterDefinition<TestEntity>>(),
                It.IsAny<FindOptions<TestEntity>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursor.Object);

        _dbMock = new Mock<IMongoDatabase>();
        _dbMock.Setup(d => d.GetCollection<TestEntity>(It.IsAny<string>(), null))
            .Returns(_collectionMock.Object);
        _serializer = BsonSerializer.SerializerRegistry.GetSerializer<TestEntity>();

        _repo = new MongoRepository<TestEntity>(_dbMock.Object);
    }

    [Test]
    public void Constructor_ThrowsException_WhenGetCollectionFails()
    {
        _dbMock.Setup(d => d.GetCollection<TestEntity>(It.IsAny<string>(), null))
            .Throws(new Exception("DB error"));

        var ex = Assert.Throws<ApplicationException>(() => new MongoRepository<TestEntity>(_dbMock.Object));
        Assert.That(ex.Message, Does.Contain("No se pudo inicializar"));
    }


    [Test]
    public async Task Find_With_Field_And_Values_Should_Call_Collection_FindAsync_With_Expected_FilterDefinition()
    {
        const string field = "Id";
        var values = new List<string> { "546c776b3e23f5f2ebdd3b03", "546c776b3e23f5f2ebdd3b04" };
        var expectedFilter = Builders<TestEntity>.Filter.In(field, values);

        await _repo.Find(field, values);

        _collectionMock.Verify(m => m.FindAsync(
            It.Is<FilterDefinition<TestEntity>>(fd => FiltersDefinitionsAreEqual(fd, expectedFilter)),
            It.IsAny<FindOptions<TestEntity>>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Find_Should_Call_Collection_FindAsync_With_Expected_FilterDefinition()
    {
        const string field = "Id";
        var value = "546c776b3e23f5f2ebdd3b04" ;
        var expectedFilter = Builders<TestEntity>.Filter.Eq(field, value);

        await _repo.Find((r) => r.Id == value);

        _collectionMock.Verify(m => m.FindAsync(
                It.Is<FilterDefinition<TestEntity>>(fd => FiltersDefinitionsAreEqual(fd, expectedFilter)),
                It.IsAny<FindOptions<TestEntity>>(),
                It.IsAny<CancellationToken>())
            , Times.Once);
    }
    
    [Test]
    public void GetCollectionName_ReturnsCorrectName()
    {
        var name = typeof(TestEntity).GetCustomAttribute<CollectionNameAttribute>()!.Name;
        var result = typeof(MongoRepository<TestEntity>)
            .GetMethod("GetCollectionName", BindingFlags.NonPublic | BindingFlags.Static)!
            .Invoke(null, [typeof(TestEntity)]);
        Assert.That(result, Is.EqualTo(name));
    }

    private bool FiltersDefinitionsAreEqual(FilterDefinition<TestEntity> definition,
        FilterDefinition<TestEntity> expectedDefinition)
    {
        var definitionRendered =
            definition.Render(new RenderArgs<TestEntity>(_serializer, BsonSerializer.SerializerRegistry));
        var expectedRendered =
            expectedDefinition.Render(new RenderArgs<TestEntity>(_serializer, BsonSerializer.SerializerRegistry));

        return definitionRendered == expectedRendered;
    }
}


[CollectionName("TestCollection")]
public class TestEntity : BaseEntity
{
    public TestEntity(string value)
    {
        Value = value;
    }

    public string Value { get; }
}