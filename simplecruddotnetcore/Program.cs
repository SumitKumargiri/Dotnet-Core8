using crudoperation.Services;
using crudoperation.Interface;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//+++++++++++++++++++++++ serilog process  +++++++++++++++++++++++++++++
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();
//++++++++++++++++++++++++++++++++++++++++++++++++++++


builder.Services.AddControllers();
string connectionstring = builder.Configuration.GetConnectionString("ConnectionString1");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//++++++++++++ with Redis +++++++++++++++++++++++++

//builder.Services.AddScoped<Icrud, CrudService>(provider =>
//{
//    var chache = provider.GetRequiredService<IMemoryCache>();
//    return new CrudService(connectionstring, chache);
//});
//builder.Services.AddMemoryCache();


//++++++++++++++++ without Redis +++++++++++++++++++++++

builder.Services.AddScoped<Icrud, CrudService>(provider =>
{
    return new CrudService(connectionstring);
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





