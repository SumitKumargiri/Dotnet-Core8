using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Google.Protobuf.WellKnownTypes;
using crudoperation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSignalR();
builder.Services.AddSignalR(e => {
    e.MaximumReceiveMessageSize = 102400000;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .WithOrigins("http://localhost:4200")
               .AllowCredentials()); 
});

builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
});

string connectionString = builder.Configuration.GetConnectionString("ConnectionString1");
//builder.Services.AddScoped<Icrud, CrudService>(provider =>
//{
//    var cache = provider.GetRequiredService<IMemoryCache>();
//    return new CrudService(connectionString, cache);
//});
//builder.Services.AddScoped<ILogin, LoginService>(provider =>
//{
//    var redisCache = provider.GetRequiredService<IDistributedCache>();
//    return new LoginService(connectionString, redisCache);
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/ChatHub").RequireCors("AllowAll"); 
});

app.UseWebSockets();

app.Run();
