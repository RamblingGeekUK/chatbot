using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ChatBot
{
    partial class Program
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
                services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader()
                           .WithOrigins("https://localhost:44365")
                           .AllowCredentials();
                }));
                services.AddSignalR();
            }

            public void Configure(IApplicationBuilder app)
            {
                app.UseRouting();
                app.UseCors("CorsPolicy");
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<ChatHub>("/chatHub");
                });
            }
        }
    }
}