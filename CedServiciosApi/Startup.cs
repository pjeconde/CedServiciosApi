using System;
//using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.IO;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;

namespace CedServiciosApi
{
    public class Startup
    {
        private const string ExceptionsOnStartup = "Startup";
        private const string ExceptionsOnConfigureServices = "ConfigureServices";
        private readonly Dictionary<string, List<Exception>> _exceptions;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this._exceptions = new Dictionary<string, List<Exception>>
                           {
                             { ExceptionsOnStartup, new List<Exception>() },
                             { ExceptionsOnConfigureServices, new List<Exception>() },
                           };
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                //services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")));
                services.AddMvc();
                services.AddDistributedMemoryCache();
                services.AddSession();

                string ambiente = Configuration.GetValue<string>("AppSettings:Ambiente");
                string dbUsuario = Configuration.GetValue<string>("AppSettings:DBUsuario");
                string dbClave = Configuration.GetValue<string>("AppSettings:DBClave");
                services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
                //string cnnStr = String.Format(Configuration.GetConnectionString("DefaultConnection"), dbUsuario, dbClave);

                string cnnStr = Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<Models.ACContext>(options => options.UseSqlServer(
                   cnnStr
                   ));

                //services.AddAuthorization();
                //System.Collections.Generic.List<CedAC.Entidades.RolSegurInf> listaRoles = db.ListaRoles();
                //string grupoAD = listaRoles[0].GrupoAD;
                //services.AddAuthorization(options =>
                //{
                //    options.AddPolicy("RequireWindowsGroupMembership", policy =>
                //    {
                //        policy.RequireAuthenticatedUser(); // Policy must have at least one requirement
                //        if (grupoAD != null)
                //            policy.RequireRole(grupoAD);
                //    });
                //});
                //services.AddAuthentication(IISDefaults.AuthenticationScheme);
                //services.AddSingleton<IPathProvider, PathProvider>();

                //JWT Validación centralizada.
                //var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
                //services.AddAuthentication(x =>
                //{
                //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //}).AddJwtBearer(x =>
                //{
                //    x.RequireHttpsMetadata = false;
                //    x.SaveToken = true;
                //    x.TokenValidationParameters = new TokenValidationParameters
                //    {
                //        ValidateIssuerSigningKey = true,
                //        IssuerSigningKey = new SymmetricSecurityKey(key),
                //        ValidateIssuer = false,
                //        ValidateAudience = false
                //    };
                //});

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "CedServicios APIs",
                        Description = "CedServicios APIs .NET Core 3.1",
                        TermsOfService = new Uri("http://www.cedeira.com.ar//"),
                        Contact = new OpenApiContact() { Name = "Pablo Conde", Email = "pjeconde@yahoo.com.ar", Url = new Uri("http://www.cedeira.com.ar//") }
                    });
                });
                var pathDocComentarios = Path.Combine(Directory.GetCurrentDirectory() + "\\CedServiciosApi.xml");
                services.AddSwaggerGen(c =>
                {
                    c.IncludeXmlComments(pathDocComentarios);
                });
            }
            catch (Exception ex)
            {
                this._exceptions[ExceptionsOnConfigureServices].Add(ex);
                if (ex.InnerException != null)
                {
                    this._exceptions[ExceptionsOnConfigureServices].Add(ex.InnerException);
                }
                return;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, Microsoft.Extensions.Options.IOptions<AppSettings> settings)
        {
            if (this._exceptions.Any(p => p.Value.Any()))
            {
                app.Run(
                  async context =>
                  {
                      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                      context.Response.ContentType = "text/html";
                      string mensaje = string.Empty;
                      mensaje = string.Format("Cantidad de excepciones en {1}:{0}<BR>", this._exceptions.Count, Environment.MachineName);
                      foreach (var ex in this._exceptions)
                      {
                          foreach (var val in ex.Value)
                          {
                              mensaje += string.Format("{0}: {1}<BR>", ex.Key, val.Message);
                          }
                      }
                      await context.Response.WriteAsync(mensaje).ConfigureAwait(false);
                  });
                return;
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CedServicios APIs");
                c.RoutePrefix = "swagger";
            });
        }
    }
}
