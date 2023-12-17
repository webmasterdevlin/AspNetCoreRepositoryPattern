/* Nullable and Implicit Usings are enabled in the csproj */

using System.Net;
using AspNetCoreRepositoryPattern.Extensions;
using AspNetCoreRepositoryPattern.Helpers;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Repositories;
using AspNetCoreRepositoryPattern.Services;
using Hangfire;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

//Read Configuration from appSettings
var serilogConfig = new ConfigurationBuilder()
    .AddJsonFile("./Logging/serilog-config.json")
    .Build();

//Initialize Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(serilogConfig)
    .CreateLogger();

builder.Host.UseSerilog();

/* Add builder.Services to the container. */
builder.Services.AddAuthorization();
builder.Services.AddControllers();

/* EF Core DbContext */
var connectionString = builder.Configuration["MSSQLServer:ConnectionString"];
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

// builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDb"));

/* Redis Cache */
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisServer:ConnectionString"];
});

/* AutoMapper for mapping Entities to Dtos */
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/* CORS */
builder.Services.AddCors();

/* API versioning */
builder.Services.AddApiVersioningExtension();
builder.Services.AddVersionedApiExplorerExtension();

/* For Open API documentation */
builder.Services.AddSwaggerGenExtension();


/* Hangfire is a timer or chron jobs or scheduled jobs */
builder.Services.AddHangfire(x => x.UseInMemoryStorage());
builder.Services.AddHangfireServer();

/* auth */
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection(nameof(AuthSettings)));

/* health checks */
builder.Services.AddHealthChecks();

/* scrutor */
builder.Services.Scan(scan =>
    scan.FromCallingAssembly()
        .AddClasses()
        .AsMatchingInterface());

/* register your contracts and repositories/builder.Services here through the built-in dependency injections */
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

/* auto map from interface to service because of scrutor e.g.
 from builder.Services.AddScoped<IPerson, Person>
 to builder.Services.AddScoped<Person> only */
builder.Services.AddScoped<TodoRepository>();
builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

/* Configure the HTTP request pipeline. */

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerExtension(provider);
}

/* CORS Policy */
app.UseCors(b =>
{
    b.AllowAnyOrigin();
    b.AllowAnyHeader();
    b.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();

/* hangfire dashboard */
app.UseHangfireDashboard("/chron-jobs-dashboard");

/* health check */
app.UseHealthChecks("/api/health");

/* Basic Global Exception Handler*/
app.UseExceptionHandler(
    options =>
    {
        options.Run(
            async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "text/html";
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                if (null != exceptionObject)
                {
                    var errorMessage = $"{exceptionObject.Error.Message}";
                    await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                }
            });
    }
);

app.MapControllers();

app.Run();