using ContratacaoService.Application.UseCase;
using ContratacaoService.Core.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "teste", Version = "v1" });
    //c.DocumentFilter<BasePathFilter>("");
});

builder.Services.AddScoped<IContratacaoUseCase, ContratacaoUseCase>();

builder.WebHost.UseUrls("http://*:5132");

var app = builder.Build();


//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();

// CORS Configurations
app.UseCors("AllowAll");

//Auth Configuration
app.MapControllers();

app.Run();
