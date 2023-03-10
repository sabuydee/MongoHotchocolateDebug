using HotChocolate.Types.Pagination;
using MongoHotchocolate;
using MongoHotchocolate.Graphql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddSingleton<DbContext>();
builder.Services
    .AddGraphQLServer()
    .AddGlobalObjectIdentification()
    .AddMongoDbPagingProviders()
    .SetPagingOptions(new PagingOptions()
    {
        IncludeTotalCount = true
    })
    .AddMongoDbProjections()
    .AddMongoDbSorting()
    .AddMongoDbFiltering()
    .AddQueryType<Query>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapGraphQL();
app.MapControllers();
app.Run();