using BoxFactory.Middleware;
using infrastructure;
using infrastructure.Repositories;
using Microsoft.Extensions.Options;
using service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string frontendRelPath = "../frontend/www/";
builder.Services.AddSpaStaticFiles(conf => conf.RootPath = frontendRelPath);
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
        dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());
}

if (builder.Environment.IsProduction())
{
    builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString);
}

builder.Services.AddSingleton<BoxRepository>();
builder.Services.AddSingleton<BoxService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

app.UseSpaStaticFiles();
app.UseSpa(conf =>
{
    conf.Options.SourcePath = frontendRelPath;
});

app.MapControllers();
app.UseMiddleware<GlobalExceptionHandler>();
app.Run();
