using System;
using System.Net;
using AspNetCoreRepositoryPattern.Contracts;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Repositories;
using AspNetCoreRepositoryPattern.Services;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddDbContext<ApplicationDbContext>((opt) => opt.UseInMemoryDatabase("InMemory"));
            
            /* AutoMapper for mapping Entities to Dtos */
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            /* For Open API documentation */
            services.AddSwaggerGen();

            /* Hangfire is a timer or chron jobs or scheduled jobs */
            services.AddHangfire(x => x.UseInMemoryStorage());
            services.AddHangfireServer();

            /* register your contracts and repositories/services here */
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IJobService, JobService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            /* Enable middleware to serve generated Swagger as a JSON endpoint. */
            app.UseSwagger();

            /*
            * Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            * specifying the Swagger JSON endpoint.
            */
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();
            
            /* hangfire dashboard */
            app.UseHangfireDashboard();
            
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
