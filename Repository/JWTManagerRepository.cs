using Atithya_Api.Models;
using Atithya_Api.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Atithya_Api.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration iconfiguration;
        public JWTManagerRepository(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public Tokens Authenticate(TokenAuthenticaton data)
        {
            if (data.TokenRequestKey is null)
                return null;

            var tokenRequestKey = iconfiguration["TokenRequestKey"];
            var dateTimeSequence = DateTime.Now.ToString("ddMMyyyy");

            var tokenAuthKey = new EncryptionHelper().EncryptPayload(tokenRequestKey + dateTimeSequence);
            if (tokenAuthKey != data.TokenRequestKey)
                return null;

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Authentication, data.TokenRequestKey) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
