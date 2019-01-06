using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestfulAspNetCore.Model.Context;
using RestfulAspNetCore.Services;
using RestfulAspNetCore.Services.Implementacoes;

namespace RestfulAspNetCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Pega a Connection String no arquivo appsettings.json
            var conexao = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySqlContext>(opcoes => opcoes.UseMySql(conexao));
            services.AddMvc();

            // Versionamento da API.
            // Referênica: https://github.com/Microsoft/aspnet-api-versioning/wiki/versioning-via-the-url-path
            // Pacote: Microsoft.AspNetCore.Mvc.Versioning
            services.AddApiVersioning();

            // Injeção de dependência
            services.AddScoped<IPessoaService, PessoaService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
