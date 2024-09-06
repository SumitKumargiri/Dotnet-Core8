using crudoperation.Services;
using crudoperation.Interface;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
string connectionstring = builder.Configuration.GetConnectionString("ConnectionString1");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var serviceToScope = new ServiceToScope(builder.Configuration);
//serviceToScope.AddToScope(builder.Services);
builder.Services.AddScoped<Icrud, CrudService>(provider =>
{
    var chache = provider.GetRequiredService<IMemoryCache>();
    return new CrudService(connectionstring, chache);
});
builder.Services.AddMemoryCache();

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





