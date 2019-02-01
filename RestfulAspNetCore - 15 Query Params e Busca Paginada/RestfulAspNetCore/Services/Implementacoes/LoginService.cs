using Microsoft.IdentityModel.Tokens;
using RestfulAspNetCore.Data.ValueObjects;
using RestfulAspNetCore.Model;
using RestfulAspNetCore.Repositories;
using RestfulAspNetCore.Security.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Services.Implementacoes
{
    public class LoginService : ILoginService
    {
        private IUsuarioRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public LoginService(IUsuarioRepository repository,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public object RetornarPorAcesso(UsuarioVo usuario)
        {
            var credenciaisValidas = false;

            if (usuario != null || !string.IsNullOrWhiteSpace(usuario.Acesso))
            {
                var usuarioBase = _repository.RetornarPorAcesso(usuario.Acesso);
                credenciaisValidas = (usuarioBase != null && usuarioBase.Senha == usuario.Senha);
            }

            if (!credenciaisValidas) return ObjetoExcessao();

            var identity = new ClaimsIdentity(
                new GenericIdentity(usuario.Acesso, "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Acesso)
                });

            var dataCriacao = DateTime.Now;
            var dataExpiracao = dataCriacao + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);
            var handler = new JwtSecurityTokenHandler();
            var token = CriarToken(identity, dataCriacao, dataExpiracao, handler);

            return ObjetoSucesso(dataCriacao, dataExpiracao, token);
        }

        private string CriarToken(ClaimsIdentity identity, DateTime dataCriacao, DateTime dataExpiracao, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.Credentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            }); // Confiurações do token.

            var token = handler.WriteToken(securityToken); // Armazena nosso token de segurana e devolve a hash de segurança.    
            return token;
        }

        private object ObjetoExcessao()
        {
            return new
            {
                Autenticated = false,
                Message = "Autenticação Falhou."
            };
        }

        private object ObjetoSucesso(DateTime dataCriacao, DateTime dataExpiracao, string token)
        {
            return new
            {
                Autenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK."
            };
        }
    }
}
