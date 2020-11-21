using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TarefasBackEnd.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TarefaBackEnd.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TarefasBackEnd
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration configuration) { // When my startup class created i will access this configurations
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); //configure in the services to add controllers

            var key = Encoding.ASCII.GetBytes("TheBigTokenCryptographyToDificultTheDecryptography");

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //my scheme of Authentication is JWT
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer( options => { //options to validate auth - validation own
                options.RequireHttpsMetadata = false;
                options.SaveToken = true; //will be save
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Symetric Key
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            //services.AddDbContext<DataContext>(options => 
             //   options.UseInMemoryDatabase("BDTarefas")); //setting de BD im memory (flash memory)

            services.AddDbContext<DataContext>(options => //psotgree no Heroku
                options.UseNpgsql(Configuration.GetConnectionString("Heroku")));

// AddScope
// AddTransient  - Create e Run that a any requeest, using persist all time requests -- For transaction
// AddSingleton - Create a one instance for class when start application - one instance for application -- For application
            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            
        }
    }
}
