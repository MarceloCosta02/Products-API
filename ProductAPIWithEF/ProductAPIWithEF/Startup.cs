using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductAPIWithEF.Data;
using Microsoft.EntityFrameworkCore;
using ProductAPIWithEF.Repository.Interfaces;
using ProductAPIWithEF.Repository.Implementations;
using ProductAPIWithEF.Business.Interfaces;
using ProductAPIWithEF.Business.Implementations;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;

namespace ProductAPIWithEF
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
            // Adicionando o contexto via lambda expression via injeção de dependencia
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "API with ASP NET Core 3.1",
                        Version = "v1"
                    });  
            });

            // Para reutilizar o DataContext existente via injeção de dependencia
            services.AddScoped<DataContext, DataContext>();

            // Para reutilizar o Repositorio via injeção de dependencia
            services.AddScoped<IProductRepository, ProductRepository>();

            // Para reutilizar o Business via injeção de dependencia
            services.AddScoped<IProductBusiness, ProductBusiness>();

            services.AddControllers();
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            
        }
    }
}
