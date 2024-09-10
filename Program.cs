using ActivoFijo.Config;
using ActivoFijo.Data;
using ActivoFijo.Middlewares;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Leer la configuración de CORS desde appsettings.json
var corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>();


// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ActivoBD"));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddCustomServices();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins(corsSettings.AllowedOrigins.ToArray())
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestInfoMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");


app.UseAuthorization();

app.MapControllers();

app.Run();
