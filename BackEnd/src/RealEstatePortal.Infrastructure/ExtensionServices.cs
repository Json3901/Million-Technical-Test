using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using RealEstatePortal.Domain.Configuration;
using RealEstatePortal.Infrastructure.Database.Interfaces;
using RealEstatePortal.Infrastructure.Database.Repositories;
using RealEstatePortal.Infrastructure.Database.Seed;
using RealStatePortal.Application.Interfaces.Infrastructure;

namespace RealEstatePortal.Infrastructure;

public static class ExtensionServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        MongoDatabaseOptions mongoDatabaseOptions)
    {
        ConventionRegistry.Register("mongoConventions", (IConventionPack)new ConventionPack()
        {
            (IConvention)new StringIdStoredAsObjectIdConvention(),
            (IConvention)new EnumRepresentationConvention(BsonType.String),
            (IConvention)new IgnoreExtraElementsConvention(true)
        }, (Func<Type, bool>)(t => true));
        services.AddScoped<IMongoDatabase>((Func<IServiceProvider, IMongoDatabase>)(serviceProvider =>
            new MongoClient(mongoDatabaseOptions.ConnectionString).GetDatabase(mongoDatabaseOptions.DatabaseName,
                (MongoDatabaseSettings)null)));
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        services.AddScoped<IPropertyRepository, PropertyRepository>();

        var provider = services.BuildServiceProvider();
        var db = provider.GetRequiredService<IMongoDatabase>();
        InitialSeed.SeedAsync(db,"data/properties.json").GetAwaiter().GetResult();

        return services;
    }
}