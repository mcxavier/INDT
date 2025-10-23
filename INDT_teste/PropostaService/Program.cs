using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PropostaService.Application.UseCase;
using PropostaService.Core.Interfaces;
using PropostaService.Persistency.Contexts;
using PropostaService.Persistency.Repository;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Logging.AddConsole();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Proposta", Version = "v1" });
    //c.DocumentFilter<BasePathFilter>("");
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IPropostaUseCase, PropostaUseCase>();
builder.Services.AddScoped<IPropostaRepository, PropostaRepository>();

builder.WebHost.UseUrls("http://*:5122");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
