using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace HIMS.API.Extensions
{
    public static class ApplicationExtension
    {
        public static void UseLinuxApacheConfiguration(this IApplicationBuilder app)
        {
            //for linux apache
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }

        public static void UseMySwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AIRMID HIMS V1");
                //c.RoutePrefix = string.Empty;
            });
        }
    }
}
