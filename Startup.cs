using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using webapi.mongodb.Data;
using webapi.mongodb.Data.Configuration;

namespace webapi.mongodb
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
            services.AddControllers();

            // MongoDB
            services.Configure<ClientesStoreDatabaseSettings>(
                            Configuration.GetSection(nameof(ClientesStoreDatabaseSettings)));

            services.AddSingleton<IClientesStoreDatabaseSettings>(sp =>
                            sp.GetRequiredService<IOptions<ClientesStoreDatabaseSettings>>().Value);

            services.AddSingleton<ClientesDb>();
            services.AddSingleton<ClientesDbAsync>();
            services.AddSingleton<ClientesDbQueryable>();

            // http://mongodb.github.io/mongo-csharp-driver/2.0/reference/driver/connecting/#re-use
            services.AddSingleton<IClientSettingsService, ClientSettingsServiceMongoDB>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
