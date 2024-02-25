using Atithya_Api.Models;

namespace Atithya_Api.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(TokenAuthenticaton data);
    }
}
