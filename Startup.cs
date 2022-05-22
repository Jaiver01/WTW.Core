using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WTW.Core.Models;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WTW.Core.Repository;

namespace WTW.Core
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
            services.AddDbContext<CoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CoreConnectionString")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WTW.Core", Version = "v1" });
            });

            services.AddCors();

            var container = new ContainerBuilder();
            container.Populate(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WTW.Core v1"));
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterInstance(Configuration).As<IConfiguration>();

            builder
                .RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterInstance(Configuration).As<IConfiguration>();
        }
    }
}
