using Foodz.API.Entitities;

namespace Foodz.API.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}