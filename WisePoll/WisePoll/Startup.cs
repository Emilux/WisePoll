using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WisePoll.Data;
using WisePoll.Data.Repositories;
using WisePoll.Services;

namespace WisePoll
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
            //BDD connect
            var cn = Configuration.GetConnectionString("mainDb");
            var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(cn));
            services.AddDbContext<ApplicationDbContext>(
                builder =>
                {
                    builder
                        .UseMySql(cn, serverVersion);
                    
                });

            services.AddHttpContextAccessor();

            services.AddAuthentication("Cookies")
                .AddCookie("Cookies", config =>
                {
                    config.LoginPath = "/auth/login";
                    config.LogoutPath = "/auth/logout";
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    config.SlidingExpiration = true;
                    config.Cookie.IsEssential = true;
                });
            
            services.AddScoped<IPollsRepository, PollsRepository>();
            services.AddScoped<IPollsService, PollsService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
