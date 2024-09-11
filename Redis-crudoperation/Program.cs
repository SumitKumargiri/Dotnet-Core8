using crudoperation.Services;
using crudoperation.Interface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Memory Cache
builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
});

string connectionString = builder.Configuration.GetConnectionString("ConnectionString1");

builder.Services.AddScoped<Icrud, CrudService>(provider =>
{
    var cache = provider.GetRequiredService<IMemoryCache>();
    return new CrudService(connectionString, cache);
});

builder.Services.AddScoped<ILogin, LoginService>(provider =>
{
    var redisCache = provider.GetRequiredService<IDistributedCache>();
    return new LoginService(connectionString, redisCache);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
