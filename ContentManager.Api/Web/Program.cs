using ContentManager.Api.Application;
using ContentManager.Api.Infrastructure.FileStorage;
using ContentManager.Api.Persistence;
using ContentManager.Api.Presentation;
using ContentManager.Api.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistance();
builder.Services.AddSecurity();
builder.Services.AddFileStorage(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();

app.UsePresentation();

await app.RunAsync();
