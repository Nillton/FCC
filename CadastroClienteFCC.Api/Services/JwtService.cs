using CadastroClienteFCC.Api.Config;
using CadastroClienteFCC.Api.Model;
using CadastroClienteFCC.Api.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroClienteFCC.Api.Services
{
    public class JwtService : IJwtService
    {
        public LoginResponseModel Authenticate(string userName, string password)
        {
            try
            {  
                var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(AppConfig.JWT_TOKEN_VALIDITY_MINS);
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(AppConfig.JWT_SECURITY_KEY);
                var securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", userName),
                    new Claim(ClaimTypes.PrimaryGroupSid, "User Group 01")
                }),
                    Expires = tokenExpiryTimeStamp,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                var token = jwtSecurityTokenHandler.WriteToken(securityToken);

                return new LoginResponseModel
                {
                    Token = token,
                    UserName = userName,
                    Expired = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ocorreu um erro ao gerar o token JWT.", ex);
            }
        }
    }
}
