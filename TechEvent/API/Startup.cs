using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TechEvent.Data.Repositories;
using TechEvent.Domain.Entities;
using TechEvent.Service;

namespace API
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
            services.AddScoped<ISponsorRepository, SponsorRepository>();
            services.AddScoped<ISponsorTypeRepository, SponsorTypeRepository>();
            services.AddScoped<ITalkRepository, TalkRepository>();
            services.AddScoped<ITalkTypeRepository, TalkTypeRepository>();
            services.AddScoped<ISpeakerRepository, SpeakerRepository>();

            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddScoped<ISponsorService, SponsorService>();
            services.AddScoped<ISponsorTypeService, SponsorTypeService>();
            services.AddScoped<ITalkService, TalkService>();
            services.AddScoped<ITalkTypeService, TalkTypeService>();
            services.AddScoped<ISpeakerService, SpeakerService>();

            services.AddDbContext<TechEventContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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



            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
