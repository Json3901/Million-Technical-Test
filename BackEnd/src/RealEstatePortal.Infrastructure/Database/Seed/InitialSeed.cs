using System.Text.Json;
using MongoDB.Driver;
using RealEstatePortal.Domain.Entities;

namespace RealEstatePortal.Infrastructure.Database.Seed;

public static class InitialSeed
{
    public static async Task SeedAsync(IMongoDatabase database, string jsonFilePath)
    {
        var seedExtecuted = await database.GetCollection<Owner>("owners").EstimatedDocumentCountAsync() > 0;
        if (seedExtecuted) return;

        var json = await File.ReadAllTextAsync(jsonFilePath);
        var seedData = JsonSerializer.Deserialize<SeedData>(json);

        await database.GetCollection<Owner>("owners").InsertManyAsync(seedData.Owners);
        await database.GetCollection<Property>("properties").InsertManyAsync(seedData.Properties);
        await database.GetCollection<PropertyImage>("property-images").InsertManyAsync(seedData.PropertyImages);
        await database.GetCollection<PropertyTrace>("property-traces").InsertManyAsync(seedData.PropertyTraces);
    }
}

public class SeedData
{
    public List<Owner> Owners { get; set; }
    public List<Property> Properties { get; set; }
    public List<PropertyImage> PropertyImages { get; set; }
    public List<PropertyTrace> PropertyTraces { get; set; }
}