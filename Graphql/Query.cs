using HotChocolate.Data;
using MongoDB.Driver;
using MongoDB.Entities;

namespace MongoHotchocolate.Graphql;

public class Query
{
    private readonly DbContext _dbContext;

    public Query(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [UsePaging]
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IExecutable<ProductCategory> ProductCategories()
    {
        var query = _dbContext.ProductCategories
            .Find(e => e.ParentProductCategoryId == null)
            .AsExecutable();
        return query;
    }
}

[Node]
public class ProductCategory : BaseEntity
{
    public string Name { get; set; }
    [ObjectId] [IsProjected] public string? ParentProductCategoryId { get; set; }
}

public class BaseEntity : Entity
{
    public string? Code { get; set; }
    public string? Description { get; set; }
}