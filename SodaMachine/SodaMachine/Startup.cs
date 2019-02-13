using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SodaMachine.Domain;
using SodaMachine.Domain.Base;
using SodaMachine.Domain.DBInit;
using SodaMachine.Domain.Repositories;
using SodaMachine.Domain.Repositories.Interfaces;
using SodaMachine.Services;

namespace SodaMachine
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationContext>(ServiceLifetime.Scoped);

            #region services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICoinsService, CoinsService>();
            services.AddScoped<ISodaService, SodaService>();
            #endregion

            #region repo
            services.AddScoped<ISodaRepository, SodaRepository>();
            services.AddScoped<ICoinRepository, CoinRepository>();
            services.AddScoped<ICoinsInMachineRepository, CoinsInMachineRepository>();
            #endregion

            #region Add db initializers
            services.AddScoped<MainDBInit>();
            #endregion
            
            services.AddCors(cors =>
            {
                cors.AddPolicy("CorsPolicy",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                );
            });

            var connection = Configuration.GetSection("ConnString").Value;

            services.AddDbContext<ApplicationContext>
                (options => options.UseSqlServer(connection));

            services.AddDirectoryBrowser();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(opts =>
            {
                // Force Camel Case to JSON
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                // Ignore circular references
                opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            var scopeFactory = services
                .BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>();

            InitializeMainDatabase(scopeFactory);
            SeedMainDatabase(scopeFactory);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ApplicationContext appContext,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            using (appContext)
            {
                appContext.Database.Migrate();
            }
        }

        private void InitializeMainDatabase(IServiceScopeFactory factory)
        {
            using (var scope = factory.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ApplicationContext>().Database.Migrate();
            }
        }

        private void SeedMainDatabase(IServiceScopeFactory factory)
        {
            using (var scope = factory.CreateScope())
            {
                 scope.ServiceProvider.GetRequiredService<MainDBInit>().Init();
            }
        }
    }
}
