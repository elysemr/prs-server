using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using prs_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prs_server
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
            
            var connStrKey = "PrsDbContextWinhost";
#if DEBUG //add this so don't have to manually change conn string depending on where you're working (local/prod)
            connStrKey = "PrsCapstoneDbContext";
#endif

            services.AddDbContext<PrsCapstoneDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString(connStrKey));

            });

            services.AddCors();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => {
            x.AllowAnyOrigin();
            x.AllowAnyHeader();
            x.AllowAnyMethod();
                            });

            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var scope = app.ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope();
            scope.ServiceProvider
                    .GetService<PrsCapstoneDbContext>()
                    .Database.Migrate();
        }
    }
}
