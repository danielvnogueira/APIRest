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
using TarefaBackEnd.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TarefasBackEnd
{
    public class Startup
    {
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

            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("BDTarefas")); //setting de BD im memory (flash memory)
// AddScope
// AddTransient  - Create e Run that a any requeest, using persist all time requests -- For transaction
// AddSingleton - Create a one instance for class when start application - one instance for application -- For application
            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
            
        }
    }
}