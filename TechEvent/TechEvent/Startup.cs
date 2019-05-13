    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;
using TechEvent.Service;

namespace TechEvent
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<ISponsorRepository, SponsorRepository>();
            services.AddScoped<ISponsorTypeRepository, SponsorTypeRepository>();
            services.AddScoped<ITalkRepository, TalkRepository>();
            services.AddScoped<ITalkTypeRepository, TalkTypeRepository>();
            services.AddScoped<ISpeakerRepository, SpeakerRepository>();
            services.AddScoped<IEditionRepository, EditionRepository>();
            services.AddScoped<IPaperRepository, PaperRepository>();
            services.AddScoped<IEditionRepository, EditionRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();

            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddScoped<ISponsorService, SponsorService>();
            services.AddScoped<ISponsorTypeService, SponsorTypeService>();
            services.AddScoped<ITalkService, TalkService>();
            services.AddScoped<ITalkTypeService, TalkTypeService>();
            services.AddScoped<ISpeakerService, SpeakerService>();
            services.AddScoped<IEditionService, EditionService>();
            services.AddScoped<IPaperService, PaperService>();
            services.AddScoped<IEditionService, EditionService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IScheduleService, ScheduleService>();

            services.AddDbContext<TechEventContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            /*
                        services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<TechEventContext>();
            */
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            /*
            services.AddMemoryCache();
            services.AddSession();
            */
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            /*
            app.UseCookiePolicy();
            app.UseSession();
            */
            app.UseAuthentication();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "areaRoute year",
                    template: "{year:int}/{area:exists}/{controller}/{action}/{id?}",
                    defaults: new { year = DateTime.Today.Year, controller = "Home", action = "Index" },
                    constraints: new { year = new EditionConstraintService() }
                    );

                routes.MapRoute(
    name: "areaRoute",
    template: "{area:exists}/{controller}/{action}/{id?}",
    defaults: new { year = DateTime.Today.Year, controller = "Home", action = "Index" }
    );

                routes.MapRoute(
                    name: "speaker details year",
                    template: "{year:int}/speakers/{pageslug}",
                    defaults: new { year = DateTime.Today.Year, controller = "Speakers", action = "Details2" },
                    constraints: new
                    {
                        pageslug = new TechEvent.Service.PageSlugConstraintService(),
                        year = new EditionConstraintService()
                    }
                    );

                routes.MapRoute(
    name: "speaker details",
    template: "speakers/{pageslug}",
    defaults: new { year = DateTime.Today.Year, controller = "Speakers", action = "Details2" },
    constraints: new
    {
        pageslug = new PageSlugConstraintService()
    }
    );

                routes.MapRoute(
                    name: "edition selector",
                    template: "{year:int}/{controller}/{action}/{id?}",
                    defaults: new { year = DateTime.Today.Year, controller = "Home", action = "Index" },
                    constraints: new { year = new EditionConstraintService() }
                    );

                routes.MapRoute(
    name: "current next edition",
    template: "{controller}/{action}/{id?}",
    defaults: new { year = DateTime.Today.Year, controller = "Home", action = "Index" }
    );

            });
        }
    }
}

/*
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
        */
