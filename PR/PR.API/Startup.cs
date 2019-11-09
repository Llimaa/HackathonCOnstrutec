﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PR.Domain.Commands.Handlers;
using Swashbuckle.AspNetCore.Swagger;

namespace PR.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddResponseCompression();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "PR", Version = "v1" });
            });

            services.AddTransient<ConstructionHandler, ConstructionHandler>();
            services.AddTransient<OwnerHandler, OwnerHandler>();
            services.AddTransient<ReportHandler, ReportHandler>();
            services.AddTransient<ResponsibleHandler, ResponsibleHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMvc(route =>
            {
                route.MapRoute("default", "{controller=swagger}/{action=Index}/{id?}");
            });
            app.UseResponseCompression();
            app.UseSwagger(c => {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PR API - v1");
                c.RoutePrefix = "swagger";
            });
            // Pagina com documentação: '/swagger/index.html'

            app.UseHttpsRedirection();

        }
    }
}
