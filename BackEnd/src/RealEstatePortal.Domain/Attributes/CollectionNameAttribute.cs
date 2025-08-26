namespace RealEstatePortal.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class CollectionNameAttribute : Attribute
{
    public CollectionNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}