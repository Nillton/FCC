using CadastroClienteFCC.Api.Model;
using CadastroClienteFCC.Api.Services.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadastroClienteFCC.Api.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly ILoginService _loginService;

        public LoginController(IJwtService jwtService, ILoginService loginService)
        {
            _jwtService = jwtService;
            _loginService = loginService;
        }

        [HttpPost]
        [Route("Autenticação")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) && string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest(new { message = "Dados de login não inseridos" });
            }

            var validaLogin = await _loginService.GetLoginUsuario(loginModel);
            if (validaLogin)
            {
                var jwtResult = _jwtService.Authenticate(loginModel.UserName, loginModel.Password);
                if (jwtResult == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(jwtResult);
                }
            }
            else
            {
                return BadRequest(new { result = "Usuario não encontrado"});
            }  
        }
    }
}
