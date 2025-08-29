using System.Linq.Expressions;
using MongoDB.Driver;
using RealEstatePortal.Domain.Attributes;
using RealEstatePortal.Domain.Entities;
using RealEstatePortal.Infrastructure.Database.Interfaces;

namespace RealEstatePortal.Infrastructure.Database.Repositories;

public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly IMongoCollection<TEntity> Collection;

    public MongoRepository(IMongoDatabase database)
    {
        try
        {
            Collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al obtener la colección '{GetCollectionName(typeof(TEntity))}': {e.Message}");
            throw new ApplicationException($"No se pudo inicializar el repositorio para la entidad '{typeof(TEntity).Name}'.", e);
        }
    }

    public async Task<TEntity> Find(Expression<Func<TEntity, bool>> filter)
    {
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> Find(FilterDefinition<TEntity> filter)
    {
        return await Collection.Find(filter).ToListAsync();
    }
    
    public async Task<List<TEntity>> Find(string field, List<string> values)
    {
        var filter = Builders<TEntity>.Filter.In(field, values);

        return await Collection.Find(filter).ToListAsync();
    }

    protected static string GetCollectionName(Type type)
    {
        var name = ((CollectionNameAttribute)type.GetCustomAttributes(typeof(CollectionNameAttribute), true)
            .FirstOrDefault()!)!.Name;
        return name;
    }
}