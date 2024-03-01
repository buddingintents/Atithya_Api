using Atithya_Api.Models;
using Atithya_Api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atithya_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IJWTManagerRepository jWTManager, ILogger<AuthenticationController> logger)
        {
            this._jWTManager = jWTManager;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult GetToken(TokenAuthenticaton data)
        {
            var token = _jWTManager.Authenticate(data.TokenRequestKey);

            _logger.LogDebug(token != null ? token.Token : "No Token Generated");
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
