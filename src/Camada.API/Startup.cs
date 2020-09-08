using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fruits.Business.Interfaces;
using Fruits.Business.Notificacoes;
using Fruits.Business.Services;
using Fruits.Data.Context;
using Fruits.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fruits.API
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
            services.AddDbContext<FruitsDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("FruitsDBContext"));
            });

            services.AddScoped<FruitsDBContext>();
            services.AddScoped<IFruitService, FruitService>();
            services.AddScoped<IFruitRepository, FruitRepository>();
            services.AddScoped<INotificator, Notificator>();
        }

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
