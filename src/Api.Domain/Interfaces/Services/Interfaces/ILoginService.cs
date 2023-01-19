using Api.Domain.DTOs;

namespace Api.Domain.Interfaces.Services.Interfaces
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDto user);
    }
}