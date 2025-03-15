using App.EnglishBuddy.API.Extensions;
using App.EnglishBuddy.API.Hubs;
using App.EnglishBuddy.Application;
using App.EnglishBuddy.Infrastructure;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddLog4Net();

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.AddSignalR();   // for signalr register


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var serviceScope = app.Services.CreateScope();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseErrorHandler();
app.UseRouting();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseStaticFiles();

// +++++++++++++++++ for signalr ++++++++++++++++++++++++
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chat");
});

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

app.UseForwardedHeaders();
app.MapControllers();
app.Run();