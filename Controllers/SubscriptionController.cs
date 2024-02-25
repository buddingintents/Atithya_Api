using Atithya_Api.Helper.Subscription;
using Atithya_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atithya_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController(ILogger<SubscriptionController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger<SubscriptionController> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost]
        [Route("[action]")]
        public APIResponse GetSubscriptionData(APIRequest data)
        {
            try
            {
                return new SubscriptionHelper(configuration).GetSubscriptionData(data);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.ToString());
                return null;
            }
        }
    }
}
