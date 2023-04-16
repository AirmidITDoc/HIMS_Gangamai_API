using HIMS.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace HIMS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLinuxApacheConfiguration(Configuration);
            services.AddMyCors();
            services.AddMyConfiguration(Configuration);
            services.AddMyAuthentication(Configuration);
            services.AddMyDependancies(Configuration);
            services.AddControllers()
                    .AddNewtonsoftJson();
            services.AddMySwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();

                //Assembly assembly = Assembly.Load(new AssemblyName(env.ApplicationName));

                //if (assembly != (Assembly)null)
                //    configuration.AddUserSecrets(assembly, true);
            }

            app.UseLinuxApacheConfiguration();
            //app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                //open swagger at root url
                endpoints.MapGet("/auth/login", async context =>
                {
                    context.Response.Redirect("/");
                });
                endpoints.MapControllers();
            });
            app.UseMySwagger();
        }
    }
}
