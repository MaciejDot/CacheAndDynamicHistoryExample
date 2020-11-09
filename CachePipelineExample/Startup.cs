using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheManager.Core;
using CachePipelineExample.Cache;
using CachePipelineExample.Core;
using CachePipelineExampleDomain.Domains.Command;
using CachePipelineExampleDomain.Domains.Query;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CachePipelineExample
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
            services.AddDbContext<ContextCore>(x => x.UseSqlServer(Configuration.GetConnectionString("core")));
            services.AddMediatR(typeof(CreateStudentCommand));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandClearer<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryCacher<,>));
            services.AddCacheManagerConfiguration(configure =>
                 configure
                     .WithMicrosoftMemoryCacheHandle()
                     .WithExpiration(ExpirationMode.Absolute, TimeSpan.FromHours(1)));
            services.AddCacheManager();
            services.AddSwaggerGen();
            //validators
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                c.RoutePrefix = string.Empty;
            });

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
