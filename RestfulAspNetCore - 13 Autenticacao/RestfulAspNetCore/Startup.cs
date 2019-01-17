using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using RestfulAspNetCore.Hypermedia;
using RestfulAspNetCore.Model.Context;
using RestfulAspNetCore.Repositories;
using RestfulAspNetCore.Repositories.Generics;
using RestfulAspNetCore.Repositories.Implementacoes;
using RestfulAspNetCore.Security.Configurations;
using RestfulAspNetCore.Services;
using RestfulAspNetCore.Services.Implementacoes;
using Swashbuckle.AspNetCore.Swagger;
using Tapioca.HATEOAS;

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

            #region Configurações de login
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations); // Adiciona como singleton para manter a instância.

            var tokenConfigurations = new TokenConfigurations();

            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura do token recebido.
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se o token ainda é valido.
                // Por exemplo, podemos receber um token que já passou do tempo de validade. Neste caso, será inválido.
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância da expiração do token.
                // Usado para problemas de sincronização entre diferentes computadores
                // envolvidos no processo de comunicação.
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Habilita o uso do token para acessar os recursos do projeto.
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(SigningConfigurations.MODO_AUTORIZACAO, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });
            #endregion

            // O Content Negociation é uma forma da API prover diversos formatos na resposta.
            // Pacote: Microsoft.AspNetCore.Mvc.Formatters.Xml
            // Ver mais detalhes em:  https://blog.jeremylikness.com/5-rest-api-designs-in-dot-net-core-1-29a8527e999chttps://blog.jeremylikness.com/5-rest-api-designs-in-dot-net-core-1-29a8527e999c
            services.AddMvc(opcoes =>
            {
                opcoes.RespectBrowserAcceptHeader = true; // Aceita o que vier no cabeçalho da requisição.
                opcoes.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                opcoes.FormatterMappings.SetMediaTypeMappingForFormat("json ", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();

            // Define as opções do filtro HATEOAS.
            var opcoesFiltro = new HyperMediaFilterOptions();
            opcoesFiltro.ObjectContentResponseEnricherList.Add(new PessoaEnricher());
            opcoesFiltro.ObjectContentResponseEnricherList.Add(new LivroEnricher());
            services.AddSingleton(opcoesFiltro);

            // Versionamento da API.
            // Referênica: https://github.com/Microsoft/aspnet-api-versioning/wiki/versioning-via-the-url-path
            // Pacote: Microsoft.AspNetCore.Mvc.Versioning
            services.AddApiVersioning(
                option => option.ReportApiVersions = true // Permite que a API retorne versões no cabeçalho de resposta.
                );

            // Swagger é um framework para documentar os nossos endpoints.
            // Bibliotecas: Swashbuckle.AspNetCore e Swashbuckle.AspNetCore.Annotations
            services.AddSwaggerGen(opcoes =>
            {
                opcoes.SwaggerDoc(
                    "v1",
                    new Info
                    {
                        Title = "RESTful API com ASP.NET Core",
                        Version = "v1"
                    });
            });

            // Injeção de dependência
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
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

            app.UseSwagger();
            app.UseSwaggerUI(opcoesSwagger =>
            {
                opcoesSwagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
            });

            var rewriteOptions = new RewriteOptions();
            rewriteOptions.AddRedirect("^$","swagger"); // Redireciona todas as requisições para a página do Swagger.
            app.UseRewriter(rewriteOptions);

            app.UseMvc(routes =>
            {
                // Rota criada para o funcionamento do HATEOAS.
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}");
            });
        }
    }
}
