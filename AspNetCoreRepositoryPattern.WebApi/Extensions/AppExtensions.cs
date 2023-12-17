using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetCoreRepositoryPattern.Extensions;

public static class AppExtensions
{
    /* Custom middleware */
        
    public static void UseSwaggerExtension(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });
    }
}