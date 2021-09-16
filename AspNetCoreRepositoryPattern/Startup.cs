using System;
using System.Net;
using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Extensions;
using AspNetCoreRepositoryPattern.Helpers;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Repositories;
using AspNetCoreRepositoryPattern.Services;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCoreRepositoryPattern
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddControllers();

            /* EF Core DbContext */
            var connectionString = Configuration["MSSQLServer:ConnectionString"];
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
            
            // services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDb"));

            /* AutoMapper for mapping Entities to Dtos */
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
           
            /* CORS */
            services.AddCors();

            /* API versioning */
            services.AddApiVersioningExtension();
            services.AddVersionedApiExplorerExtension();
            
            /* For Open API documentation */
            services.AddSwaggerGenExtension();
            
            
            /* Hangfire is a timer or chron jobs or scheduled jobs */
            services.AddHangfire(x => x.UseInMemoryStorage());
            services.AddHangfireServer();
            


            /* register your contracts and repositories/services here */
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IJobService, JobService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
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


            app.UseAuthorization();
            
            /* hangfire dashboard */
            app.UseHangfireDashboard("/chron-jobs-dashboard");
            
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
