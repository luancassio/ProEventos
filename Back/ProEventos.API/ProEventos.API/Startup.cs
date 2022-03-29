using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProEventos.Application.Class;
using ProEventos.Application.Dto;
using ProEventos.Application.Interface;
using ProEventos.Application.Services;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Class;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace ProEventos.API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddDbContext<ProEventosContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<User>(opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 4;

            }).AddRoles<Role>()
              .AddRoleManager<RoleManager<Role>>()
              .AddSignInManager<SignInManager<User>>()
              .AddRoleValidator<RoleValidator<Role>>()
              .AddEntityFrameworkStores<ProEventosContext>()
              .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                        ValidateIssuer =  false,
                        ValidateAudience = false
                        
                    };
                });

            // configura��o para ignora os loops de relacionamento das classes
            services.AddControllers()
                              .AddJsonOptions(options =>
                                  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                              )
                              .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
                                  Newtonsoft.Json.ReferenceLoopHandling.Ignore
                              );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IGenericsPersistence, GenericsPersistence>();

            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<ILotePersistence, LotePersistence>();

            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<IEventoPersistence, EventoPersistence>();

            services.AddScoped<IPalestranteService, PalestranteService>();
            services.AddScoped<IPalestrantePersistence, PalestrantePersistence>();

            services.AddScoped<IUserPersistence, UserPersistence>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();





            services.AddCors();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = @"JWT Authorization header usando Bearer.
                                    Entre com 'Bearer' [espa�o] ent�o coloque seu token
                                    Exemplo: 'Bearer a124ff4df45754ds54fd5'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                         new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                         }, new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseStaticFiles(new StaticFileOptions() {
                FileProvider = new 
                PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
