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
using RestfulAspNetCore.Repositories;
using RestfulAspNetCore.Repositories.Generics;
using RestfulAspNetCore.Repositories.Implementacoes;
using RestfulAspNetCore.Services;
using RestfulAspNetCore.Services.Implementacoes;

namespace RestfulAspNetCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Environment = environment;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Pega a Connection String no arquivo appsettings.json
            var connectionString = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySqlContext>(opcoes => opcoes.UseMySql(connectionString));
            ExecutarMigration(connectionString);
            services.AddMvc();

            // Versionamento da API.
            // Referênica: https://github.com/Microsoft/aspnet-api-versioning/wiki/versioning-via-the-url-path
            // Pacote: Microsoft.AspNetCore.Mvc.Versioning
            services.AddApiVersioning();

            // Injeção de dependência
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<ILivroService, LivroService>();

            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        private void ExecutarMigration(string connectionString)
        {
            // Verifica em qual ambiente está executando: produção, desenvolvimento, etc.
            if (Environment.IsDevelopment())
            {
                try
                {
                    // Foi necessário criar um alias no arquivo csproj para resolver o conflito de versões entre o MySql.Data e o Pomelo.
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve("Evolve.json", evolveConnection, mensagem => _logger.LogInformation(mensagem))
                    {
                        Locations = new List<string>() { "Db/Migrations", "Db/Dataset" }, // Nomes das migrations.
                        IsEraseDisabled = true // Para não apagar os dados toda vez que rodar.
                    };

                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database Migration falhou.", ex);
                    throw ex;
                }
            }
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
