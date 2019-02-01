using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Security.Configurations
{
    public class SigningConfigurations
    {
        public const string MODO_AUTORIZACAO = "Bearer";

        public SecurityKey Key { get; }
        public SigningCredentials Credentials { get; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            Credentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
