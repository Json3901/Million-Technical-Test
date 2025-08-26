using System.Linq.Expressions;
using MongoDB.Driver;
using RealEstatePortal.Domain.Entities;

namespace RealEstatePortal.Infrastructure.Database.Interfaces;

public interface IMongoRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> Find(Expression<Func<TEntity, bool>> filter);
    Task<List<TEntity>> Find(string field, List<string> values);
    Task<List<TEntity>> Find(FilterDefinition<TEntity> filter);
}