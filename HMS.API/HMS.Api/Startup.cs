﻿using AutoMapper;
using HMS.Api.Models;
using HMS.Api.Repositories;
using HMS.Api.Repositories.HMSDb;
using HMS.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;
using HMS.Api.Options;
using Microsoft.OpenApi.Models;

namespace HMS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });


            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddMvc()
                .AddJsonOptions(
                    options => {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.Formatting = Formatting.Indented;
                    }
                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<AuthenticationContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDbContext<HMSDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            }
            );

            services.AddCors(o => o.AddPolicy("MyPolicy", builder => {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HMSApi", Version = "v1" });
            });

            //Jwt Authentication

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"]);

            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            RegisterRepositories(services);
        }

        private void RegisterRepositories(IServiceCollection services) {
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {

            app.Use(async (ctx, next) => {
                await next();
                if (ctx.Response.StatusCode == 204) {
                    ctx.Response.ContentLength = 0;
                }
            });

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(options => { options.RouteTemplate = swaggerOptions.JsonRoute; });

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(swaggerOptions.UIEndPoint, swaggerOptions.Description);
            });

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
