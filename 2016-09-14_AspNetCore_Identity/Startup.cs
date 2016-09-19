using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace _2016_09_14_AspNetCore_Identity
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

			// 添加策略
			services.AddAuthorization(options =>
			{
				options.AddPolicy("LoginOnly",
							 policy => policy.RequireClaim("UserName"));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();
			// my own identity based on Microsoft.AspNetCore.Authentication.Cookies
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationScheme = "MyCookieMiddlewareInstance",    // this scheme name
				LoginPath = new PathString("/Account/Register/"),   // if has not been authenticated, redirect to login path
																		// PathString: provides correct escoping path
				AccessDeniedPath = new PathString("/Account/Forbidden/"),   // if user attmpt to access but does not pass the authorization policies
				AutomaticAuthenticate = true,   // true: middleware run on every request and attempt to validate and reconstruct any serialized principal it created
				AutomaticChallenge = true   // challenge 质询, redirect browser to the LoginPath/AccessDeniedPath when authorization fails.
			});
			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

		}
    }
}
