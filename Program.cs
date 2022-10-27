using InitalWebAPI.Contexts;
using InitalWebAPI.Helpers.SettingOptions;
using InitalWebAPI.Middlewares;
using InitalWebAPI.Repositories.Implements;
using InitalWebAPI.Repositories.Interfaces;
using InitalWebAPI.Services.Implements;
using InitalWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Services to the project
builder.Services.AddScoped<IDogService, DogService>();
builder.Services.AddScoped<IDogRepository, DogRepository>();
builder.Services.AddTransient<ErrorHandlerMiddleware>();

// Register Http Context Accessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Register factories
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Db ConnectionString
builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection(ConnectionStringOptions.ConnectionStrings));
var connectionStringOptions = new ConnectionStringOptions();
builder.Configuration.GetSection(ConnectionStringOptions.ConnectionStrings).Bind(connectionStringOptions);

var connectionString = connectionStringOptions.ServiceDatabase;
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<ProjectContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

builder.Services.AddCors();

//Build app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
            options.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
