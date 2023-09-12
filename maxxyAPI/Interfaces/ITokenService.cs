using maxxyAPI.Entities;

namespace maxxyAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
