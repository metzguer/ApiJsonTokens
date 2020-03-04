using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApiPaises.Connexion;
using WebApiPaises.Entities;
using WebApiPaises.Repository;

namespace WebApiPaises
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
            services.AddDbContext<ConnectDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionDatabase")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ConnectDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
            
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "misitioweb.com",
                    ValidAudience = "misitioweb.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["key_toekn_json"]) ),
                    ClockSkew = TimeSpan.Zero
                }
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(ConfigureJson);

            services.AddTransient<IPasesRepository, PaisesRepository>();
            services.AddTransient <IProvinceRepository, ProvinceRepository>();
        }

        private void ConfigureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ConnectDbContext context)
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
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            ////seeds 
            //if (!context.Paises.Any())
            //{
            //    context.Paises.AddRange(new List<Pais>() {
            //        new Pais(){ Name="Mexico", Capital="Mexico City",
            //            Provinces = new List<Province>(){ new Province() { Name="Guerrero"}, new Province() {Name="Guanajuato" } }
            //        },
            //        new Pais(){ Name="Cuba", Capital="La Habana",
            //            Provinces = new List<Province>(){ new Province() { Name = "Venezuela" }, new Province() { Name = "Bolivia" } }
            //        }
            //    } );

            //}
            //end seeds
        }
    }
}
